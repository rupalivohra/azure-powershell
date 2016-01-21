﻿using Microsoft.CLU;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Management.Automation.Runspaces
{
    /// <summary>
    /// The pipe that uses IConsoleInputOutput (Out-Proc-Pipe)
    /// </summary>
    internal class ConsolePipe : PipeBase
    {
        /// <summary>
        /// Backing field for the property Reader.
        /// </summary>
        PipelineReader<string> _reader;
        /// <summary>
        /// The pipe reader.
        /// </summary>
        public override PipelineReader<string> Reader
        {
            get
            {
                if (_reader == null)
                {
                    _reader = new PipelineStringReader(this);
                }

                return _reader;
            }
        }

        /// <summary>
        /// Backing field for the property Writer.
        /// </summary>
        PipelineWriter _writer;
        /// <summary>
        /// The pipe writer.
        /// </summary>
        public override PipelineWriter Writer
        {
            get
            {
                if (_writer == null)
                {
                    _writer = new PipelineStringWriter(this);
                }

                return _writer;
            }
        }

        /// <summary>
        /// The maximum capacity of the pipe.
        /// </summary>
        public override int MaxCapacity
        {
            get
            {
                return int.MaxValue;
            }
        }

        /// <summary>
        /// Indicates whether the pipe is open or not.
        /// </summary>
        public override bool IsOpen
        {
            get
            {
                return !_closed;
            }
        }

        /// <summary>
        /// The number of items in the pipe.
        /// </summary>
        public override int Count
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Creates an instance of ConsolePipe.
        /// </summary>
        /// <param name="consoleInputOutput">The information of console to use</param>
        public ConsolePipe(IConsoleInputOutput consoleInputOutput) : base(consoleInputOutput.Input, consoleInputOutput.PipelineOutput, consoleInputOutput.IsOutputRedirected)
        {
        }

        /// <summary>
        /// Read next string from the pipe.
        /// </summary>
        /// <returns>The next string item in the pipe</returns>
        public override string Read()
        {
            Debug.Assert(!_closed);
            string line = base.Read();
            return line;
        }

        /// <summary>
        /// Read a sequence of string items from the pipe.
        /// </summary>
        /// <param name="count">The number of items to read</param>
        /// <returns>The collection of string items</returns>
        public override Collection<string> Read(int count)
        {
            Debug.Assert(!_closed);
            var lines = base.Read(count);
            return lines;
        }

        /// <summary>
        /// Read all string items from the pipe.
        /// </summary>
        /// <returns>The collection of string items</returns>
        public override Collection<string> ReadToEnd()
        {
            Debug.Assert(!_closed);
            var lines = base.ReadToEnd();
            return lines;
        }

        /// <summary>
        /// Do non-blocking read.
        /// </summary>
        /// <returns></returns>
        public override Collection<string> NonBlockingRead()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Do non-blocking read.
        /// </summary>
        /// <param name="maxRequested">The maximum string items to be read</param>
        /// <returns></returns>
        public override Collection<string> NonBlockingRead(int maxRequested)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Read next string item without consuming it.
        /// </summary>
        /// <returns>The next string item</returns>
        public override string Peek()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Mark the pipe as readable.
        /// </summary>
        public override void SetReadable()
        {
            // Console streams are always readable.
        }

        /// <summary>
        /// Write the given object as json string to pipe.
        /// </summary>
        /// <param name="obj">The object to write as json string</param>
        public override void Write(object obj)
        {
            Debug.Assert(!_closed);
            base.Write(obj);
        }

        /// <summary>
        /// Write the given enumerable object as json strings to pipe.
        /// </summary>
        /// <param name="obj">The object to enumerate and write</param>
        /// <param name="enumerateCollection">Indicates whether to enumerate or not</param>
        /// <returns>The number of objects written</returns>
        public override int Write(object obj, bool enumerateCollection)
        {
            Debug.Assert(!_closed);
            return base.Write(obj, enumerateCollection);
        }

        /// <summary>
        /// Flushes the pipe.
        /// </summary>
        public override void Flush()
        {
            if (!_closed)
            {
                base.Flush();
            }
        }

        /// <summary>
        /// Mark the pipe as writable.
        /// </summary>
        public override void SetWritable()
        {
            // Console streams are always writable.
        }

        /// <summary>
        /// Close the pipe.
        /// </summary>
        public override void Close()
        {
            _closed = true;
        }

        /// <summary>
        /// Closes the read end.
        /// </summary>
        public override void ReadClose()
        {
        }

        /// <summary>
        /// Closes the write end.
        /// </summary>
        public override void WriteClose()
        {
        }

        #region Private fields

        /// <summary>
        /// Indicates whether the pipe is closed or not.
        /// </summary>
        private bool _closed;

        #endregion
    }
}