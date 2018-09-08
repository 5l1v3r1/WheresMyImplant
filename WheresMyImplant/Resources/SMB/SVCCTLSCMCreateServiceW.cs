﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WheresMyImplant
{
    class SVCCTLSCMCreateServiceW
    {
        private Byte[] ContextHandle;
        private Byte[] ServiceName_MaxCount;
        private Byte[] ServiceName_Offset = { 0x00, 0x00, 0x00, 0x00 };
        private Byte[] ServiceName_ActualCount;
        private Byte[] ServiceName;
        private Byte[] DisplayName_ReferentID;
        private Byte[] DisplayName_MaxCount;
        private readonly Byte[] DisplayName_Offset = { 0x00, 0x00, 0x00, 0x00 };
        private Byte[] DisplayName_ActualCount;
        private Byte[] DisplayName;
        private readonly Byte[] AccessMask = { 0xff, 0x01, 0x0f, 0x00 };
        private readonly Byte[] ServiceType = { 0x10, 0x00, 0x00, 0x00 };
        private readonly Byte[] ServiceStartType = { 0x03, 0x00, 0x00, 0x00 };
        private readonly Byte[] ServiceErrorControl = { 0x00, 0x00, 0x00, 0x00 };
        private Byte[] BinaryPathName_MaxCount;
        private readonly Byte[] BinaryPathName_Offset = { 0x00, 0x00, 0x00, 0x00 };
        private Byte[] BinaryPathName_ActualCount;
        private Byte[] BinaryPathName;
        private readonly Byte[] NULLPointer = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] TagID = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] NULLPointer2 = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] DependSize = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] NULLPointer3 = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] NULLPointer4 = { 0x00, 0x00, 0x00, 0x00 };
        private readonly Byte[] PasswordSize = { 0x00, 0x00, 0x00, 0x00 };

        internal SVCCTLSCMCreateServiceW()
        {
            DisplayName_ReferentID = Misc.Combine(BitConverter.GetBytes(Misc.GenerateUuidNumeric(2)), new Byte[] { 0x00, 0x00 });
        }
        
        internal void SetContextHandle(Byte[] ContextHandle)
        {
            this.ContextHandle = ContextHandle;
        }

        internal void SetServiceName()
        {
            SetServiceName(Misc.GenerateUuidAlpha(20));
        }

        internal void SetServiceName(String strServiceName)
        {
            Byte[] tmp = Misc.Combine(Encoding.Unicode.GetBytes(strServiceName), new Byte[] { 0x00, 0x00 });
            if (0 != strServiceName.Length % 2)
            {
                tmp = Misc.Combine(tmp, new Byte[] { 0x00, 0x00 });
            }
            ServiceName = DisplayName = tmp;

            ServiceName_MaxCount = ServiceName_ActualCount = DisplayName_MaxCount = DisplayName_ActualCount = BitConverter.GetBytes(strServiceName.Length + 1);
        }

        internal void SetCommand(String command)
        {
            BinaryPathName = Encoding.Unicode.GetBytes(command);
            BinaryPathName_ActualCount = BinaryPathName_MaxCount = BitConverter.GetBytes(command.Length + 1);
        }

        internal Byte[] GetRequest()
        {
            Combine combine = new Combine();
            combine.Extend(ContextHandle);
            combine.Extend(ServiceName_MaxCount);
            combine.Extend(ServiceName_Offset);
            combine.Extend(ServiceName_ActualCount);
            combine.Extend(ServiceName);
            combine.Extend(DisplayName_ReferentID);
            combine.Extend(DisplayName_MaxCount);
            combine.Extend(DisplayName_Offset);
            combine.Extend(DisplayName_ActualCount);
            combine.Extend(DisplayName);
            combine.Extend(AccessMask);
            combine.Extend(ServiceType);
            combine.Extend(ServiceStartType);
            combine.Extend(ServiceErrorControl);
            combine.Extend(BinaryPathName_MaxCount);
            combine.Extend(BinaryPathName_Offset);
            combine.Extend(BinaryPathName_ActualCount);
            combine.Extend(BinaryPathName);
            combine.Extend(NULLPointer);
            combine.Extend(TagID);
            combine.Extend(NULLPointer2);
            combine.Extend(DependSize);
            combine.Extend(NULLPointer3);
            combine.Extend(NULLPointer4);
            combine.Extend(PasswordSize);
            return combine.Retrieve();
        }
    }
}