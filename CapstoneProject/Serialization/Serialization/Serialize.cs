using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Polenter.Serialization;

namespace CustomSerialization
{
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
    /// Serialize<TestClass> serializer = new Serialize<TestClass>();
    /// TestClass test = new TestClass("jon", "AU");
    ///
    /// How to save:
    ///    serializer.Save(test);
    ///
    /// How to Load:
    ///     test = serializer.Load("Savegame.xml");
    ///
    /// NOTE: Only public class variables are serialized
    ///         Also must have a defualt contructor, i.e one with no parameters
    ///
    /// All saved files should be "My documents\SavedGames\Project\Mario_Hack-N-Slash\player1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Serialize<T>
    {
        //generic class type, stores the object passed in by the constructor
        T _classType;

        SharpSerializer complexSerializer;
        StorageContainer _container = null;
        StorageDevice _device = null;

        //Name of file
        private string _fileName = "Savegame.xml";

        // Needed variables
        IAsyncResult _result = null;

        XmlSerializer serializer;
        Stream stream = null;
        bool _complex = true;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Serialize()
        {
        }

        public T ClassType
        {
            get { return _classType; }
            set { _classType = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// Loads from a xml file given the fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T Load(string fileName)
        {
            if (_complex)
            {
                complexSerializer = new SharpSerializer();
                try
                {
                    _classType = (T)complexSerializer.Deserialize("XMLfiles/" + fileName);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                complexSerializer = null;

                return _classType;
            }
            _fileName = fileName;
            this.preLoadSave();
            if (!_container.FileExists(_fileName))
            {
                _container.Dispose();
                return _classType;
            }

            // Create the file.
            // Convert the object to XML data
            // and put it in the stream.
            stream = _container.OpenFile(_fileName, FileMode.Open);
            _classType = (T)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();
            // Dispose the container, to
            // commit changes.
            _container.Dispose();

            return _classType;
        }

        /// <summary>
        /// Saves to an XML file from passed in class
        ///
        /// Complex parameter is optional : Pass true if serializing does not work correctly without it
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="complex"></param>
        public void Save(T classType, bool complex = true)
        {
            _complex = complex;

            if (_complex)
            {
                complexSerializer = new SharpSerializer();
                complexSerializer.Serialize(classType, "XMLfiles/" + this._fileName);
                _classType = classType;
                //try
                //{
                //    _classType = (T)complexSerializer.Deserialize("s.xml");
                //}
                //catch (Exception e)
                //{
                //}
            }
            else
            {
                serializer = new XmlSerializer(typeof(T));

                _classType = classType;
                this.preLoadSave();

                // Check to see whether
                // the file exists.
                if (_container.FileExists(_fileName))
                {
                    // Delete it so that we
                    // can create one fresh.
                    _container.DeleteFile(_fileName);
                }

                // Create the file.
                // Convert the object to XML data
                // and put it in the stream.

                stream = _container.CreateFile(_fileName);
                serializer.Serialize(stream, _classType);

                // Close the file.
                stream.Close();
                // Dispose the container, to
                // commit changes.
                _container.Dispose();
            }
        }

        /// <summary>
        /// Common code between Load and save
        /// </summary>
        private void preLoadSave()
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
        }
    }
}