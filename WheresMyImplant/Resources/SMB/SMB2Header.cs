﻿using System;
using System.Linq;
using System.Security.Cryptography;

namespace WheresMyImplant
{
    class SMB2Header
    {
        private readonly Byte[] ServerComponent = { 0xfe, 0x53, 0x4d, 0x42 };
        private readonly Byte[] HeaderLength = { 0x40, 0x00 };
        private readonly Byte[] CreditCharge = { 0x01, 0x00 };
        private Byte[] ChannelSequence = new Byte[2];
        private readonly Byte[] Reserved = { 0x00, 0x00 };
        private Byte[] Command = new Byte[2];
        private Byte[] CreditsRequested = new Byte[2];
        private Byte[] Flags = new Byte[4];
        private Byte[] ChainOffset = new Byte[4];
        private Byte[] MessageID = new Byte[8];
        private Byte[] ProcessId = new Byte[4];
        private Byte[] TreeId = new Byte[4];
        private Byte[] SessionId = new Byte[8];
        private Byte[] Signature = new Byte[16];

        internal SMB2Header()
        {
            ChannelSequence = new Byte[] { 0x00, 0x00 };
            Flags = new Byte[] { 0x00, 0x00, 0x00, 0x00 };
            ChainOffset = new Byte[] { 0x00, 0x00, 0x00, 0x00 };
        }

        internal void SetCommand(Byte[] command)
        {
            if (command.Length == this.Command.Length)
            {
                this.Command = command;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetCreditsRequested(Byte[] creditsRequested)
        {
            if (creditsRequested.Length == this.CreditsRequested.Length)
            {
                this.CreditsRequested = creditsRequested;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetFlags(Byte[] flags)
        {
            if (flags.Length == this.Flags.Length)
            {
                this.Flags = flags;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetMessageID(UInt32 messageId)
        {
            this.MessageID = Misc.Combine(BitConverter.GetBytes(messageId), new Byte[] { 0x00, 0x00, 0x00, 0x00 });
        }

        internal void SetProcessID(Byte[] processId)
        {
            if (processId.Length == this.ProcessId.Length)
            {
                this.ProcessId = processId;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetTreeId(Byte[] treeId)
        {
            if (treeId.Length == this.TreeId.Length)
            {
                this.TreeId = treeId;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetSessionID(Byte[] sessionId)
        {
            if (sessionId.Length == this.SessionId.Length)
            {
                this.SessionId = sessionId;
                return;
            }
            throw new IndexOutOfRangeException();
        }

        internal void SetSignature(Byte[] sessionKey, ref Byte[] data)
        {
            using (HMACSHA256 sha256 = new HMACSHA256())
            {
                sha256.Key = sessionKey;
                this.Signature = sha256.ComputeHash(Misc.Combine(GetHeader(), data)).Take(16).ToArray();
            }
        }

        internal Byte[] GetHeader()
        {
            Byte[] header = Misc.Combine(ServerComponent, HeaderLength);
            header = Misc.Combine(header, CreditCharge);
            header = Misc.Combine(header, ChannelSequence);
            header = Misc.Combine(header, Reserved);
            header = Misc.Combine(header, Command);
            header = Misc.Combine(header, CreditsRequested);
            header = Misc.Combine(header, Flags);
            header = Misc.Combine(header, ChainOffset);
            header = Misc.Combine(header, MessageID);
            header = Misc.Combine(header, ProcessId);
            header = Misc.Combine(header, TreeId);
            header = Misc.Combine(header, SessionId);
            header = Misc.Combine(header, Signature);
            return header;
        }
    }
}