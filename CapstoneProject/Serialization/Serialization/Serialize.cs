using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Serialization
{
    public class Serialize<T> : Microsoft.Xna.Framework.DrawableGameComponent
    {
        T _classType;
        private SpriteBatch _spriteBatch;
        private KeyboardState _currentState;

        private KeyboardState _previousState;

        private string _fileName = "Savegame.xml";

        private enum State
        {
            Innactive, RequestDevice,
            GetDevice, GetContainer,
            Save, Load
        };

        private State _state;

        private enum Action { None, Save, Load };

        private Action _action;

        // Needed variables
        IAsyncResult _result = null;

        StorageDevice _device = null;
        StorageContainer _container = null;

        /// <summary>
        /// Constructor: Initialize game variable in base class
        /// </summary>
        /// <param name="game"></param>
        public Serialize(Game game, T classType)
            : base(game)
        {
            _state = State.Innactive;
            _action = Action.None;
            _classType = classType;
        }

        public override void Initialize()
        {
            base.Initialize();
            this._spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            // Cheack what keys are presesed
            _previousState = _currentState;
            _currentState = Keyboard.GetState();

            // STEP 1: User requests save / load

            // When we press the "s" key, We want to save
            if (_currentState.IsKeyUp(Keys.S) &&
            _previousState.IsKeyDown(Keys.S))
            {
                // Request to save
                if (_state == State.Innactive && _action == Action.None)
                {
                    _state = State.RequestDevice;
                    _action = Action.Save;
                }
            }

            // When we press the "l" key, we want to load
            if (_currentState.IsKeyUp(Keys.L) &&
            _previousState.IsKeyDown(Keys.L))
            {
                // Request to save
                if (_state == State.Innactive &&
                _action == Action.None)
                {
                    _state = State.RequestDevice;
                    _action = Action.Load;
                }
            }

            // Do the actual save or load
            if (_action == Action.Save &&
            _state != State.Innactive)
            {
                this.Save();
            }
            //if (_action == Action.Load &&
            //_state != State.Innactive)
            //{
            //    this.Load();
            //}
        }

        /// <summary>
        /// Function where we do all the saving
        /// </summary>
        private void Save()
        {
            switch (_state)
            {
                case State.RequestDevice:
                    {
                        // STEP 2.a: Request the device
                        _result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                        _state = State.GetDevice;
                    }
                    break;
                case State.GetDevice:
                    {
                        // If the device is ready
                        if (_result.IsCompleted)
                        {
                            // STEP 2.b: Recieve the device
                            _device = StorageDevice.EndShowSelector(_result);
                            // STEP 3.a: Request the container, wait for it to complete
                            _result.AsyncWaitHandle.Close();
                            //clear result
                            _result = null;
                            //store StorageDevice in result with fileName being the name of the file
                            _result = _device.BeginOpenContainer("Storage", null, null);
                            _state = State.GetContainer;
                        }
                    }
                    break;
                case State.GetContainer:
                    {
                        // If the container is ready
                        if (_result.IsCompleted)
                        {
                            // STEP 3.b: Recieve the container
                            _container = _device.EndOpenContainer(_result);
                            _result.AsyncWaitHandle.Close();
                            _state = State.Save;
                        }
                    }
                    break;
                case State.Save:
                    {
                        // Check to see whether
                        // the file exists.
                        if (_container.FileExists(_fileName))
                        {
                            // Delete it so that we
                            // can create one fresh.
                            _container.DeleteFile(_fileName);
                        }
                        // Create the file.
                        Stream stream = _container.CreateFile(_fileName);
                        // Convert the object to XML data
                        // and put it in the stream.
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(stream, _classType);
                        // Close the file.
                        stream.Close();
                        // Dispose the container, to
                        // commit changes.
                        _container.Dispose();
                        _state = State.Innactive;
                        _action = Action.None;
                    }
                    break;
            }
        }
    }
}