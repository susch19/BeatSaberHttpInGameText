using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BS_Utils.Gameplay;
using HttpText.Models;
using HttpText.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HttpText.Core
{
    public class TextCreator : MonoBehaviour
    {

        private static event EventHandler<string> NewHttpText;

        private Canvas _canvas;
        private TextMeshProUGUI _text;
        private static HttpListener listener;
        private static Task listenerTask;
        private static CancellationTokenSource token;

        static TextCreator()
        {
            Logger.log.Debug("Creating Http Listener");

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8880/");
            listener.Prefixes.Add("http://127.0.0.1:8880/");
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            Logger.log.Debug("Creating Listener Thread");
            listener.Start();
            token = new CancellationTokenSource();
            listenerTask = new Task(startListener, token.Token, token.Token);
            listenerTask.Start();
            Logger.log.Debug("Started Listener Thread");
        }

        private void Start()
        {
            Logger.log.Debug("Loading HttpText");
            PrepareWarningText();
        }

        private void PrepareWarningText()
        {
            Logger.log.Debug("Prepare");
            var is360Level = BS_Utils.Plugin.LevelData?.GameplayCoreSceneSetupData?.difficultyBeatmap?.beatmapData?.spawnRotationEventsCount;
            Vector3 pos;
            Quaternion rot;
            if (is360Level.HasValue)
            {
                pos = is360Level.Value > 0 ? Float3.ToVector3(Plugin.config.Value.HttpText360LevelPosition) : Float3.ToVector3(Plugin.config.Value.HttpTextStandardLevelPosition);
                rot = is360Level.Value > 0
                    ? Quaternion.Euler(Float3.ToVector3(Plugin.config.Value.HttpText360LevelRotation))
                    : Quaternion.Euler(Float3.ToVector3(Plugin.config.Value.HttpTextStandardLevelRotation));
            }
            else
            {
                pos = Float3.ToVector3(Plugin.config.Value.MenuTextStandardLevelPosition);
                rot = Quaternion.Euler(Float3.ToVector3(Plugin.config.Value.MenuTextStandardLevelPosition));
            }

            _canvas = new GameObject("DiffWarningCanvas").AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.transform.position = pos;
            _canvas.transform.rotation = rot;
            _canvas.transform.localScale /= 100;
            Logger.log.Debug("Canvas created");
            var rectTransform = _canvas.transform as RectTransform;
            if (rectTransform != null)
                rectTransform.sizeDelta = new Vector2(140, 50);
            Logger.log.Debug("rectTransform created");
            _text = Utils.CreateText((RectTransform)_canvas.transform,
                                     "HttpText Ready",
                                     new Vector2());
            Logger.log.Debug("Text created");
            _text.alignment = TextAlignmentOptions.Center;
            _text.fontSize = 16f;
            _text.alpha = 1f;
            Logger.log.Debug("Text configured");
            _canvas.enabled = true;
            NewHttpText += TextCreator_NewHttpText;
        }

        private void TextCreator_NewHttpText(object sender, string e)
        {
            _text.text = e;
        }

        private static void startListener(object state)
        {
            var t = (CancellationToken)state;
            if (t.IsCancellationRequested)
                return;
            listener.BeginGetContext((r) => ListenerCallback(r, t), listener);
        }

        private static void ListenerCallback(IAsyncResult result, CancellationToken t)
        {
            var context = listener.EndGetContext(result);
            if (context.Request.HttpMethod == "POST")
            {
                if (context.Request.InputStream != null)
                    using (var sr = new StreamReader(context.Request.InputStream, Encoding.UTF8))
                    {
                        var data_text = sr.ReadToEnd();
                        NewHttpText?.Invoke(null, data_text);
                    }
                else
                {
                    Logger.log.Debug("Got a null stream");
                    NewHttpText?.Invoke(null, "Error");
                }
            }
            context.Response.Close();
            startListener(t);
        }

        private void Stop()
        {
            NewHttpText -= TextCreator_NewHttpText;
            _text.text = "";
        }

        private void OnDestroy()
        {
            NewHttpText -= TextCreator_NewHttpText;
            _text.text = "";
        }

        private void Update()
        {

        }
    }
}