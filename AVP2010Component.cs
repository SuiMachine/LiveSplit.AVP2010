﻿using LiveSplit.Model;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.AVP2010
{
    class AVP2010Component : LogicComponent
    {
        public override string ComponentName
        {
            get { return "AVP2010"; }
        }

        protected InfoTimeComponent InternalComponent { get; set; }
        public AVP2010Settings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public AVP2010Component(LiveSplitState state, bool isLayoutComponent)
        {
            _timer = new TimerModel { CurrentState = state };
            _state = state;
            _timer.CurrentState.OnStart += timer_OnStart;

            _gameMemory = new GameMemory(this.Settings);
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            state.OnStart += State_OnStart;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            _timer.CurrentState.OnStart -= timer_OnStart;
            _state.OnStart -= State_OnStart;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }

        }

        private void timer_OnStart(object sender, EventArgs e)
        {
            _timer.InitializeGameTime();
        }

        void State_OnStart(object sender, EventArgs e)
        {
            _gameMemory.resetSplitStates();
        }

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _timer.CurrentState.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _timer.CurrentState.IsGameTimePaused = false;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("Settings");
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public override void SetSettings(XmlNode settings)
        {
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}
