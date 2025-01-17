﻿using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Parsec.Common;
using Parsec.Extensions;
using Parsec.Helpers;
using Parsec.Readers;
using Parsec.Shaiya.Data;

namespace Parsec.Shaiya.Core
{
    public abstract class FileBase : IFileBase, IExportable<FileBase>
    {
        [JsonIgnore]
        protected SBinaryReader _binaryReader;

        /// <summary>
        /// The file's byte array
        /// </summary>
        [JsonIgnore]
        public byte[] Buffer
        {
            get => _binaryReader.Buffer;
            set => _binaryReader = new SBinaryReader(value);
        }

        /// <summary>
        /// Full path to the file
        /// </summary>
        [JsonIgnore]
        public string Path { get; set; } = "";

        [JsonIgnore]
        public abstract string Extension { get; }

        /// <summary>
        /// Plain file name
        /// </summary>
        [JsonIgnore]
        public string FileName => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// File name without the extension (.xx)
        /// </summary>
        [JsonIgnore]
        public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Path);

        public FileBase(SBinaryReader binaryReader)
        {
        }

        [JsonConstructor]
        public FileBase()
        {
        }

        /// <summary>
        /// Reads the shaiya file format from a file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="options">Array of reading options</param>
        /// <typeparam name="T">Shaiya File Format Type</typeparam>
        /// <returns>T instance</returns>
        public static T ReadFromFile<T>(string path, params object[] options) where T : FileBase, new()
        {
            var binaryReader = new SBinaryReader(path);

            var instance = new T
            {
                Path = path,
                _binaryReader = binaryReader
            };

            if (instance is IEncryptable encryptableInstance)
                encryptableInstance.DecryptBuffer();

            // Parse the file
            instance.Read(options);
            return instance;
        }

        /// <summary>
        /// Reads the shaiya file format from a buffer (byte array)
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="buffer">File buffer</param>
        /// <param name="options">Array of reading options</param>
        /// <typeparam name="T">Shaiya File Format Type</typeparam>
        /// <returns>T instance</returns>
        public static T ReadFromBuffer<T>(string name, byte[] buffer, params object[] options) where T : FileBase, new()
        {
            var binaryReader = new SBinaryReader(buffer);

            var instance = new T
            {
                Path = name,
                _binaryReader = binaryReader
            };

            if (instance is IEncryptable encryptableInstance)
                encryptableInstance.DecryptBuffer();

            // Parse the file
            instance.Read(options);
            return instance;
        }

        public static T ReadFromData<T>(Data.Data data, SFile file, params object[] options) where T : FileBase, new()
        {
            if (!data.FileIndex.ContainsValue(file))
                throw new FileNotFoundException("The provided SFile instance is not part of the Data");

            return ReadFromBuffer<T>(file.Name, data.GetFileBuffer(file), options);
        }

        /// <inheritdoc/>
        public abstract void Read(params object[] options);

        /// <inheritdoc />
        public virtual void Write(string path, params object[] options) =>
            FileHelper.WriteFile(path, GetBytes(options));

        /// <inheritdoc />
        public abstract byte[] GetBytes(params object[] options);

        /// <inheritdoc/>
        public void ExportJson(string path, params string[] ignoredPropertyNames) =>
            FileHelper.WriteFile(path, Encoding.ASCII.GetBytes(JsonSerialize(this, ignoredPropertyNames)));

        /// <inheritdoc/>
        public virtual string JsonSerialize(FileBase obj, params string[] ignoredPropertyNames)
        {
            // Create settings with contract resolver to ignore certain properties
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PropertyFilterCamelCaseResolver(ignoredPropertyNames),
                DefaultValueHandling = DefaultValueHandling.Include
            };

            // Add enum to string converter
            settings.Converters.Add(new StringEnumConverter());

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
