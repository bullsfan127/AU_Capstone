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
    //Actions that are passed in for the Action parameter
    public enum Actions { None, Save, Load };

    /// <summary>
    /// This class Takes a class, and converts it to an xml file that is saved on the disk. It also has the ability to load from those saved files
    /// to create a class.
    /// This is essentially a 6 step process for both loading and saving.
    ///     1. User requests to Save / Load
    ///     2. Get the StorageDevice.
    ///         2a. Request StorageDevice
    ///         2b. Receive StorageDevice
    ///     3. Get a StorageContainer from the device.
    ///         3a. Request StorageContainer
    ///         3b. Receive StorageContainer
    ///     4. Get the Stream of the file we want to use from the container.
    ///     5. Serialize / Deserialize.
    ///     6. Cleanup.
    ///
    /// This is a generic class, so it should be able to take any class type and convert and load to and from an xml file
    ///
    /// Examples:
    ///
    /// How to save:
    ///    TestClass test = new TestClass("jon", "AU");
    ///    new Serialize<TestClass>(TestClassObject, Action.Save);
    ///
    /// How to Load:
    ///     Serialize<TestClass> s = new Serialize<TestClass>(test, Action.Load);
    ///     TestClass test = s.ClassType;
    ///
    /// NOTE: Only public class variables are serialized
    ///         Also must have a defualt contructor, i.e one with no parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Serialize<T>
    {
        //generic class type, stores the object passed in by the constructor
        T _classType;

        public T ClassType
        {
            get { return _classType; }
            set { _classType = value; }
        }

        //Name of file
        private string _fileName = "Savegame.xml";

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        //Holds the action, i.e Load or Save
        private Actions _action;

        // Needed variables
        IAsyncResult _result = null;

        StorageDevice _device = null;
        StorageContainer _container = null;

        /// <summary>
        /// Initializes the Serialize object
        /// Then calls SaveLoad()
        /// Step 1.
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="action"></param>
        public Serialize(T classType, Actions action)
        {
            _classType = classType;
            _action = action;
            this.SaveLoad();
        }

        /// <summary>
        /// Function where we do all the saving or loading
        /// </summary>
        private void SaveLoad()
        {
            // STEP 2.a: Request the device
            _result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            // If the device is ready
            if (_result.IsCompleted)
            {
                // STEP 2.b: Recieve the device
                _device = StorageDevice.EndShowSelector(_result);
                // STEP 3.a: Request the container, wait for it to complete
                _result.AsyncWaitHandle.Close();
                //clear result
                _result = null;
                //store StorageDevice in result with the name of the folder
                _result = _device.BeginOpenContainer("Mario_Hack-N-Slash", null, null);
            }

            // If the container is ready
            if (_result.IsCompleted)
            {
                // STEP 3.b: Recieve the container
                _container = _device.EndOpenContainer(_result);
                _result.AsyncWaitHandle.Close();
            }

            // Check to see whether
            // the file exists.
            if (_container.FileExists(_fileName))
            {
                // Delete it so that we
                // can create one fresh.
                if (_action == Actions.Save)
                    _container.DeleteFile(_fileName);
                if (!_container.FileExists(_fileName) && _action == Actions.Load)
                {
                    _container.Dispose();
                    return;
                }
            }
            Stream stream = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            // Create the file.
            // Convert the object to XML data
            // and put it in the stream.
            if (_action == Actions.Save)
            {
                stream = _container.CreateFile(_fileName);
                serializer.Serialize(stream, _classType);
            }
            else if (_action == Actions.Load)
            {
                stream = _container.OpenFile(_fileName, FileMode.Open);
                _classType = (T)serializer.Deserialize(stream);
            }
            // Close the file.
            stream.Close();
            // Dispose the container, to
            // commit changes.
            _container.Dispose();
        }
    }
}