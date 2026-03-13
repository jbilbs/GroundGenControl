using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MccDaq
{
	internal class CbwApi
	{
		private delegate int cbGetRevisionP(out float revNum, out float vxDRevNum);

		private delegate int cbLoadConfigP(string cfgFileName);

		private delegate int cbSaveConfigP(string cfgFileName);

		private delegate int cbErrHandlingP(int errReporting, int errHandling);

		private delegate int cbFileGetInfoP(string fileName, out short lowChan, out short highChan, out int pretrigCount, out int totalCount, out int rate, out int gain);

		private delegate int cbFileRead_ObsoleteP(string fileName, int firstPoint, ref int numPoints, out short dataBuffer);

		private delegate int cbWinBufToArray_ObsoleteP(int memHandle, out short dataArray, int startPoint, int count);

		private delegate int cbWinBufToArray32_ObsoleteP(int memHandle, out int dataArray, int startPoint, int count);

		private delegate int cbScaledWinBufToArray_ObsoleteP(int memHandle, out double dataArray, int startPoint, int count);

		private delegate int cbWinArrayToBuf_ObsoleteP(ref short DataArray, int MemHandle, int StartPt, int Count);

		private delegate int cbScaledWinArrayToBuf_ObsoleteP(ref double DataArray, int MemHandle, int StartPt, int Count);

		private delegate int cbWinBufAlloc_ObsoleteP(int NumPoints);

		private delegate int cbWinBufAlloc32_ObsoleteP(int NumPoints);

		private delegate int cbWinBufAlloc64_ObsoleteP(int NumPoints);

		private delegate int cbScaledWinBufAlloc_ObsoleteP(int NumPoints);

		private delegate int cbWinBufFree_ObsoleteP(int MemHandle);

		private delegate int cbDeclareRevisionP(ref float RevNum);

		private delegate int cbGetBoardNameP(int BoardNum, [MarshalAs(UnmanagedType.VBByRefStr)] ref string BoardName);

		private delegate int cbGetErrMsgP(int ErrCode, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ErrMsg);

		private delegate int cbACalibrateData_ObsoleteP(int BoardNum, int NumPoints, int Gain, ref short ADData);

		private delegate int cbAConvertData_ObsoleteP(int BoardNum, int NumPoints, ref short ADData, out short ChanTags);

		private delegate int cbAConvertPretrigData_ObsoleteP(int BoardNum, int PreTrigCount, int TotalCount, ref short ADData, out short ChanTags);

		private delegate int cbAInP(int BoardNum, int Chan, int Gain, out short DataValue);

		private delegate int cbAIn32P(int BoardNum, int Chan, int Gain, out int DataValue, int Options);

		private delegate int cbAInScan_ObsoleteP(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, int MemHandle, int Options);

		private delegate int cbALoadQueueP(int BoardNum, short[] ChanArray, short[] GainArray, int Count);

		private delegate int cbAOutP(int BoardNum, int Chan, int Gain, ushort DataValue);

		private delegate int cbAOutScan_ObsoleteP(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, int MemHandle, int Options);

		private delegate int cbAPretrig_ObsoleteP(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, int MemHandle, int Options);

		private delegate int cbATrigP(int BoardNum, int Chan, int TrigType, short TrigValue, int Gain, out short DataValue);

		private delegate int cbC7266ConfigP(int BoardNum, int CounterNum, int Quadrature, int CountingMode, int DataEncoding, int IndexMode, int InvertIndex, int FlagPins, int GateEnable);

		private delegate int cbC8254ConfigP(int BoardNum, int CounterNum, int Config);

		private delegate int cbC8536ConfigP(int BoardNum, int CounterNum, int OutputControl, int RecycleMode, int TrigType);

		private delegate int cbC9513ConfigP(int BoardNum, int CounterNum, int GateControl, int CounterEdge, int CountSource, int SpecialGate, int Reload, int RecycleMode, int BCDMode, int CountDirection, int OutputControl);

		private delegate int cbC8536InitP(int BoardNum, int ChipNum, int Ctr1Output);

		private delegate int cbC9513InitP(int BoardNum, int ChipNum, int FOutDivider, int FOutSource, int Compare1, int Compare2, int TimeOfDay);

		private delegate int cbCFreqInP(int BoardNum, int SigSource, int GateInterval, out short Count, out int Freq);

		private delegate int cbCInP(int BoardNum, int CounterNum, out short Count);

		private delegate int cbCIn32P(int BoardNum, int CounterNum, out int Count);

		private delegate int cbCIn64P(int BoardNum, int CounterNum, out long Count);

		private delegate int cbCLoadP(int BoardNum, int RegNum, uint LoadValue);

		private delegate int cbCLoad32P(int BoardNum, int RegNum, uint LoadValue);

		private delegate int cbCLoad64P(int BoardNum, int RegNum, ulong LoadValue);

		private delegate int cbCStatusP(int BoardNum, int CounterNum, out uint StatusBits);

		private delegate int cbCStoreOnInt_ObsoleteP(int BoardNum, int IntCount, ref CounterControl CntrControl, int MemHandle);

		private delegate int cbCInScan_ObsoleteP(int BoardNum, int FirstCtr, int LastCtr, int Count, ref int Rate, int MemHandle, uint Options);

		private delegate int cbCConfigScanP(int BoardNum, int CounterNum, uint Mode, uint DebounceTime, uint DebounceTrigger, uint EdgeDetection, uint TickSize, uint MapCounter);

		private delegate int cbCClearP(int BoardNum, int CounterNum);

		private delegate int cbTimerOutStartP(int BoardNum, int TimerNum, ref double Frequency);

		private delegate int cbTimerOutStopP(int BoardNum, int TimerNum);

		private delegate int cbPulseOutStartP(int BoardNum, int TimerNum, ref double Frequency, ref double DutyCycle, uint PulseCount, ref double InitialDelay, int IdleState, int Options);

		private delegate int cbPulseOutStopP(int BoardNum, int TimerNum);

		private delegate int cbDBitInP(int BoardNum, int PortType, int BitNum, out ushort BitValue);

		private delegate int cbDBitOutP(int BoardNum, int PortType, int BitNum, ushort BitValue);

		private delegate int cbDConfigPortP(int BoardNum, int PortNum, int Direction);

		private delegate int cbDConfigBitP(int BoardNum, int PortNum, int BitNum, int Direction);

		private delegate int cbDInP(int BoardNum, int PortNum, out short DataValue);

		private delegate int cbDInScan_ObsoleteP(int BoardNum, int PortNum, int Count, ref int Rate, int MemHandle, int Options);

		private delegate int cbDOutP(int BoardNum, int PortNum, ushort DataValue);

		private delegate int cbDOutScan_ObsoleteP(int BoardNum, int PortNum, int Count, ref int Rate, int MemHandle, int Options);

		private delegate int cbFileAInScanP(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, string FileName, int Options);

		private delegate int cbFilePretrigP(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, string FileName, int Options);

		private delegate int cbFlashLEDP(int BoardNum);

		private delegate int cbGetIOStatusP(int BoardNum, out short Status, out int CurCount, out int CurIndex, int FunctionType);

		private delegate int cbRS485P(int BoardNum, int Transmit, int Receive);

		private delegate int cbStopIOBackgroundP(int BoardNum, int FunctionType);

		private delegate int cbTInP(int BoardNum, int Chan, int Scale, out float TempValue, int Options);

		private delegate int cbTInScan_ObsoleteP(int BoardNum, int LowChan, int HighChan, int Scale, out float DataBuffer, int Options);

		private delegate int cbMemSetDTModeP(int BoardNum, int Mode);

		private delegate int cbMemResetP(int BoardNum);

		private delegate int cbMemRead_ObsoleteP(int BoardNum, out short DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemWrite_ObsoleteP(int BoardNum, ref short DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemReadPretrig_ObsoleteP(int BoardNum, out short DataBuffer, int FirstPoint, int Count);

		private delegate int cbInByteP(int BoardNum, int PortNum);

		private delegate int cbOutByteP(int BoardNum, int PortNum, int PortVal);

		private delegate int cbInWordP(int BoardNum, int PortNum);

		private delegate int cbOutWordP(int BoardNum, int PortNum, int PortVal);

		private delegate int cbGetConfigStringP(int InfoType, int BoardNum, int DevNum, int ConfigItem, StringBuilder ConfigVal, ref int MaxConfigLen);

		private delegate int cbGetConfigP(int InfoType, int BoardNum, int DevNum, int ConfigItem, out int ConfigVal);

		private delegate int cbSetConfigStringP(int InfoType, int BoardNum, int DevNum, int ConfigItem, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ConfigVal, ref int MaxConfigLen);

		private delegate int cbSetConfigP(int InfoType, int BoardNum, int DevNum, int ConfigItem, int ConfigVal);

		private delegate int cbToEngUnitsP(int BoardNum, int Range, ushort DataVal, out float EngUnits);

		private delegate int cbToEngUnits32P(int BoardNum, int Range, uint DataVal, out double EngUnits);

		private delegate int cbFromEngUnitsP(int BoardNum, int Range, float EngUnits, out short DataVal);

		private delegate int cbSetTriggerP(int BoardNum, int TrigType, ushort LowThreshold, ushort HighThreshold);

		private delegate int cbEnableEvent_nonclsP(int BoardNum, uint EventType, uint Count, EventCallback CallbackFunc, IntPtr UserData);

		private delegate int cbEnableEventP(int BoardNum, uint EventType, uint Count, CallbackFunction CallbackFunc, IntPtr UserData);

		private delegate int cbDisableEventP(int BoardNum, uint EventType);

		private delegate int cbSelectSignalP(int BoardNum, int Direction, int Signal, int Connection, int Polarity);

		private delegate int cbGetSignalP(int BoardNum, int Direction, int Signal, int Index, out int Connection, out int Polarity);

		private delegate int cbSetCalCoeffP(int BoardNum, int FunctionType, int Channel, int Range, int Item, int Value, int Store);

		private delegate int cbGetCalCoeffP(int BoardNum, int FunctionType, int Channel, int Range, int Item, out int Value);

		private delegate int cbWinBufToEngUnits_ObsoleteP(int BoardNum, short[] GainArray, int GainCount, int MemHandle, out float EngUnits, int FirstPoint, int Count);

		private delegate int cbWinBufFromEngUnits_ObsoleteP(int BoardNum, short[] GainArray, int GainCount, ref float EngUnits, int MemHandle, int FirstPoint, int Count);

		private delegate int cbDaqInScan_ObsoleteP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, ref int PretrigCount, ref int TotalCount, int MemHandle, int Options);

		private delegate int cbDaqSetTriggerP(int BoardNum, int TrigSource, int TrigSense, int TrigChan, int ChanType, int Gain, float Level, float Variance, int TrigEvent);

		private delegate int cbDaqSetSetpointsP(int BoardNum, float[] LimitAArray, float[] LimitBArray, float[] reserved, int[] SetpointFlagsArray, int[] SetpointOutputArray, float[] Output1Array, float[] Output2Array, float[] OutputMask1Array, float[] OutputMask2Array, int SetpointCount);

		private delegate int cbDaqOutScan_ObsoleteP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, int Count, int MemHandle, int Options);

		private delegate int cbGetTCValues_ObsoleteP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, int ChanCount, int MemHandle, int FirstPoint, int Count, int Scale, out float TempValArray);

		private delegate int cbVInP(int BoardNum, int Chan, int Gain, out float DataValue, int Options);

		private delegate int cbVIn32P(int BoardNum, int Chan, int Gain, out double DataValue, int Options);

		private delegate int cbVOutP(int BoardNum, int Chan, int Gain, float DataValue, int Options);

		private delegate int cbResetDeviceP(int BoardNum);

		private delegate int cbDeviceLoginP(int BoardNum, [MarshalAs(UnmanagedType.VBByRefStr)] ref string AccountName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Password);

		private delegate int cbDeviceLogoutP(int BoardNum);

		private delegate int cbTEDSReadP(int BoardNum, int Chan, byte[] DataBuffer, ref int Count, int Options);

		private delegate int cbLogSetPreferencesP(TimeFormat timeFormat, TimeZone timeZone, TempScale units);

		private delegate int cbLogGetPreferencesP(ref TimeFormat timeFormat, ref TimeZone timeZone, ref TempScale units);

		private delegate int cbLogGetFileNameP(int fileNumber, [MarshalAs(UnmanagedType.VBByRefStr)] ref string path, [MarshalAs(UnmanagedType.VBByRefStr)] ref string filename);

		private delegate int cbLogGetFileInfoP(string filename, ref int version, ref int size);

		private delegate int cbLogGetSampleInfoP(string filename, ref int sampleInterval, ref int sampleCount, ref int startDate, ref int startTime);

		private delegate int cbLogGetAIChannelCountP(string filename, ref int aiCount);

		private delegate int cbLogGetAIInfoP(string filename, [MarshalAs(UnmanagedType.LPArray)] int[] channelNumbers, [MarshalAs(UnmanagedType.LPArray)] int[] units);

		private delegate int cbLogGetCJCInfoP(string filename, ref int cjcCount);

		private delegate int cbLogGetDIOInfoP(string filename, ref int dioCount);

		private delegate int cbLogReadTimeTagsP(string filename, int startSample, int count, [MarshalAs(UnmanagedType.LPArray)] int[] dateTags, [MarshalAs(UnmanagedType.LPArray)] int[] timeTags);

		private delegate int cbLogReadAIChannelsP(string filename, int startSample, int count, [MarshalAs(UnmanagedType.LPArray)] float[] aiChannels);

		private delegate int cbLogReadCJCChannelsP(string filename, int startSample, int count, [MarshalAs(UnmanagedType.LPArray)] float[] cjcChannels);

		private delegate int cbLogReadDIOChannelsP(string filename, int startSample, int count, [MarshalAs(UnmanagedType.LPArray)] int[] dioChannels);

		private delegate int cbLogConvertFileP([MarshalAs(UnmanagedType.VBByRefStr)] ref string srcFilename, [MarshalAs(UnmanagedType.VBByRefStr)] ref string destFileName, int startSample, int count, FieldDelimiter delimiter);

		private delegate int cbACalibrateData_noncls_ObsoleteP(int BoardNum, int NumPoints, int Gain, ref ushort ADData);

		private delegate int cbAConvertData_noncls_ObsoleteP(int BoardNum, int NumPoints, ref ushort ADData, out ushort ChanTags);

		private delegate int cbAConvertPretrigData_noncls_ObsoleteP(int BoardNum, int PreTrigCount, int TotalCount, ref ushort ADData, out ushort ChanTags);

		private delegate int cbAIn_nonclsP(int BoardNum, int Chan, int Gain, out ushort DataValue);

		private delegate int cbAIn32_nonclsP(int BoardNum, int Chan, int Gain, out uint DataValue, int Options);

		private delegate int cbATrig_nonclsP(int BoardNum, int Chan, int TrigType, ushort TrigValue, int Gain, out ushort DataValue);

		private delegate int cbCFreqIn_nonclsP(int BoardNum, int SigSource, int GateInterval, out ushort Count, out int Freq);

		private delegate int cbCIn_nonclsP(int BoardNum, int CounterNum, out ushort Count);

		private delegate int cbCIn32_nonclsP(int BoardNum, int CounterNum, out uint Count);

		private delegate int cbCIn64_nonclsP(int BoardNum, int CounterNum, out ulong Count);

		private delegate int cbDIn_nonclsP(int BoardNum, int PortNum, out ushort DataValue);

		private delegate int cbMemWrite_noncls_ObsoleteP(int BoardNum, ref ushort DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemRead_noncls_ObsoleteP(int BoardNum, out ushort DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemReadPretrig_noncls_ObsoleteP(int BoardNum, out ushort DataBuffer, int FirstPoint, int Count);

		private delegate int cbFromEngUnits_nonclsP(int BoardNum, int Range, float EngUnits, out ushort DataVal);

		private delegate int cbFileRead_noncls_ObsoleteP(string fileName, int firstPoint, ref int numPoints, out ushort dataBuffer);

		private delegate int cbWinBufToArray_noncls_ObsoleteP(int memHandle, out ushort dataArray, int startPoint, int count);

		private delegate int cbWinBufToArray32_noncls_ObsoleteP(int memHandle, out uint dataArray, int startPoint, int count);

		private delegate int cbWinArrayToBuf_noncls_ObsoleteP(ref ushort DataArray, int MemHandle, int StartPt, int Count);

		private delegate int cbACalibrateDataP(int BoardNum, int NumPoints, int Gain, short[] ADData);

		private delegate int cbAConvertDataP(int BoardNum, int NumPoints, short[] ADData, short[] ChanTags);

		private delegate int cbAConvertPretrigDataP(int BoardNum, int PreTrigCount, int TotalCount, short[] ADData, short[] ChanTags);

		private delegate int cbAInScanP(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, IntPtr MemHandle, int Options);

		private delegate int cbAOutScanP(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, IntPtr MemHandle, int Options);

		private delegate int cbAPretrigP(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, IntPtr MemHandle, int Options);

		private delegate int cbCStoreOnIntP(int BoardNum, int IntCount, CounterControl[] CntrControl, IntPtr MemHandle);

		private delegate int cbCInScanP(int BoardNum, int FirstCtr, int LastCtr, int Count, ref int Rate, IntPtr MemHandle, uint Options);

		private delegate int cbDInScanP(int BoardNum, int PortNum, int Count, ref int Rate, IntPtr MemHandle, int Options);

		private delegate int cbDOutScanP(int BoardNum, int PortNum, int Count, ref int Rate, IntPtr MemHandle, int Options);

		private delegate int cbWinBufToEngUnitsP(int BoardNum, short[] GainArray, int GainCount, IntPtr MemHandle, float[] EngUnits, int FirstPoint, int Count);

		private delegate int cbWinBufFromEngUnitsP(int BoardNum, short[] GainArray, int GainCount, float[] EngUnits, IntPtr MemHandle, int FirstPoint, int Count);

		private delegate int cbDaqInScanP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, ref int PretrigCount, ref int TotalCount, IntPtr MemHandle, int Options);

		private delegate int cbDaqOutScanP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, int Count, IntPtr MemHandle, int Options);

		private delegate int cbGetTCValuesP(int BoardNum, short[] ChanArray, short[] ChanTypeArray, int ChanCount, IntPtr MemHandle, int FirstPoint, int Count, int Scale, float[] TempValArray);

		private delegate int cbTInScanP(int BoardNum, int LowChan, int HighChan, int Scale, float[] DataBuffer, int Options);

		private delegate int cbMemReadP(int BoardNum, short[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemWriteP(int BoardNum, short[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemReadPretrigP(int BoardNum, short[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbFileReadP(string fileName, int firstPoint, ref int numPoints, short[] dataBuffer);

		private delegate int cbWinBufToArrayP(IntPtr memHandle, short[] dataArray, int startPoint, int count);

		private delegate int cbWinBufToArray32P(IntPtr memHandle, int[] dataArray, int startPoint, int count);

		private delegate int cbScaledWinBufToArrayP(IntPtr memHandle, double[] dataArray, int startPoint, int count);

		private delegate int cbWinArrayToBufP(short[] DataArray, IntPtr MemHandle, int StartPt, int Count);

		private delegate int cbScaledWinArrayToBufP(double[] DataArray, IntPtr MemHandle, int StartPt, int Count);

		private delegate IntPtr cbWinBufAllocP(int NumPoints);

		private delegate IntPtr cbWinBufAlloc32P(int NumPoints);

		private delegate IntPtr cbWinBufAlloc64P(int NumPoints);

		private delegate IntPtr cbScaledWinBufAllocP(int NumPoints);

		private delegate int cbWinBufFreeP(IntPtr memHandle);

		private delegate int cbACalibrateData_nonclsP(int BoardNum, int NumPoints, int Gain, ushort[] ADData);

		private delegate int cbAConvertData_nonclsP(int BoardNum, int NumPoints, ushort[] ADData, ushort[] ChanTags);

		private delegate int cbAConvertPretrigData_nonclsP(int BoardNum, int PreTrigCount, int TotalCount, ushort[] ADData, ushort[] ChanTags);

		private delegate int cbMemWrite_nonclsP(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemRead_nonclsP(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbMemReadPretrig_nonclsP(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count);

		private delegate int cbFileRead_nonclsP(string fileName, int firstPoint, ref int numPoints, ushort[] dataBuffer);

		private delegate int cbWinBufToArray_nonclsP(IntPtr memHandle, ushort[] dataArray, int startPoint, int count);

		private delegate int cbWinBufToArray32_nonclsP(IntPtr memHandle, uint[] dataArray, int startPoint, int count);

		private delegate int cbWinArrayToBuf_nonclsP(ushort[] DataArray, IntPtr MemHandle, int StartPt, int Count);

		private static CbwApi m_instance;

		private static object m_syncRoot = new object();

		private static IntPtr m_cbwDll;

		private cbGetRevisionP _cbGetRevision;

		private cbLoadConfigP _cbLoadConfig;

		private cbSaveConfigP _cbSaveConfig;

		private cbErrHandlingP _cbErrHandling;

		private cbFileGetInfoP _cbFileGetInfo;

		private cbFileRead_ObsoleteP _cbFileRead_Obsolete;

		private cbWinBufToArray_ObsoleteP _cbWinBufToArray_Obsolete;

		private cbWinBufToArray32_ObsoleteP _cbWinBufToArray32_Obsolete;

		private cbScaledWinBufToArray_ObsoleteP _cbScaledWinBufToArray_Obsolete;

		private cbWinArrayToBuf_ObsoleteP _cbWinArrayToBuf_Obsolete;

		private cbScaledWinArrayToBuf_ObsoleteP _cbScaledWinArrayToBuf_Obsolete;

		private cbWinBufAlloc_ObsoleteP _cbWinBufAlloc_Obsolete;

		private cbWinBufAlloc32_ObsoleteP _cbWinBufAlloc32_Obsolete;

		private cbWinBufAlloc64_ObsoleteP _cbWinBufAlloc64_Obsolete;

		private cbScaledWinBufAlloc_ObsoleteP _cbScaledWinBufAlloc_Obsolete;

		private cbWinBufFree_ObsoleteP _cbWinBufFree_Obsolete;

		private cbDeclareRevisionP _cbDeclareRevision;

		private cbGetBoardNameP _cbGetBoardName;

		private cbGetErrMsgP _cbGetErrMsg;

		private cbACalibrateData_ObsoleteP _cbACalibrateData_Obsolete;

		private cbAConvertData_ObsoleteP _cbAConvertData_Obsolete;

		private cbAConvertPretrigData_ObsoleteP _cbAConvertPretrigData_Obsolete;

		private cbAInP _cbAIn;

		private cbAIn32P _cbAIn32;

		private cbAInScan_ObsoleteP _cbAInScan_Obsolete;

		private cbALoadQueueP _cbALoadQueue;

		private cbAOutP _cbAOut;

		private cbAOutScan_ObsoleteP _cbAOutScan_Obsolete;

		private cbAPretrig_ObsoleteP _cbAPretrig_Obsolete;

		private cbATrigP _cbATrig;

		private cbC7266ConfigP _cbC7266Config;

		private cbC8254ConfigP _cbC8254Config;

		private cbC8536ConfigP _cbC8536Config;

		private cbC9513ConfigP _cbC9513Config;

		private cbC8536InitP _cbC8536Init;

		private cbC9513InitP _cbC9513Init;

		private cbCFreqInP _cbCFreqIn;

		private cbCInP _cbCIn;

		private cbCIn32P _cbCIn32;

		private cbCIn64P _cbCIn64;

		private cbCLoadP _cbCLoad;

		private cbCLoad32P _cbCLoad32;

		private cbCLoad64P _cbCLoad64;

		private cbCStatusP _cbCStatus;

		private cbCStoreOnInt_ObsoleteP _cbCStoreOnInt_Obsolete;

		private cbCInScan_ObsoleteP _cbCInScan_Obsolete;

		private cbCConfigScanP _cbCConfigScan;

		private cbCClearP _cbCClear;

		private cbTimerOutStartP _cbTimerOutStart;

		private cbTimerOutStopP _cbTimerOutStop;

		private cbPulseOutStartP _cbPulseOutStart;

		private cbPulseOutStopP _cbPulseOutStop;

		private cbDBitInP _cbDBitIn;

		private cbDBitOutP _cbDBitOut;

		private cbDConfigPortP _cbDConfigPort;

		private cbDConfigBitP _cbDConfigBit;

		private cbDInP _cbDIn;

		private cbDInScan_ObsoleteP _cbDInScan_Obsolete;

		private cbDOutP _cbDOut;

		private cbDOutScan_ObsoleteP _cbDOutScan_Obsolete;

		private cbFileAInScanP _cbFileAInScan;

		private cbFilePretrigP _cbFilePretrig;

		private cbFlashLEDP _cbFlashLED;

		private cbGetIOStatusP _cbGetIOStatus;

		private cbRS485P _cbRS485;

		private cbStopIOBackgroundP _cbStopIOBackground;

		private cbTInP _cbTIn;

		private cbTInScan_ObsoleteP _cbTInScan_Obsolete;

		private cbMemResetP _cbMemReset;

		private cbMemRead_ObsoleteP _cbMemRead_Obsolete;

		private cbMemWrite_ObsoleteP _cbMemWrite_Obsolete;

		private cbMemReadPretrig_ObsoleteP _cbMemReadPretrig_Obsolete;

		private cbInByteP _cbInByte;

		private cbOutByteP _cbOutByte;

		private cbInWordP _cbInWord;

		private cbOutWordP _cbOutWord;

		private cbGetConfigStringP _cbGetConfigString;

		private cbGetConfigP _cbGetConfig;

		private cbSetConfigStringP _cbSetConfigString;

		private cbSetConfigP _cbSetConfig;

		private cbToEngUnitsP _cbToEngUnits;

		private cbToEngUnits32P _cbToEngUnits32;

		private cbFromEngUnitsP _cbFromEngUnits;

		private cbSetTriggerP _cbSetTrigger;

		private cbEnableEvent_nonclsP _cbEnableEvent_noncls;

		private cbEnableEventP _cbEnableEvent;

		private cbDisableEventP _cbDisableEvent;

		private cbSelectSignalP _cbSelectSignal;

		private cbGetSignalP _cbGetSignal;

		private cbSetCalCoeffP _cbSetCalCoeff;

		private cbGetCalCoeffP _cbGetCalCoeff;

		private cbWinBufToEngUnits_ObsoleteP _cbWinBufToEngUnits_Obsolete;

		private cbWinBufFromEngUnits_ObsoleteP _cbWinBufFromEngUnits_Obsolete;

		private cbDaqInScan_ObsoleteP _cbDaqInScan_Obsolete;

		private cbDaqSetTriggerP _cbDaqSetTrigger;

		private cbDaqSetSetpointsP _cbDaqSetSetpoints;

		private cbDaqOutScan_ObsoleteP _cbDaqOutScan_Obsolete;

		private cbGetTCValues_ObsoleteP _cbGetTCValues_Obsolete;

		private cbVInP _cbVIn;

		private cbVIn32P _cbVIn32;

		private cbVOutP _cbVOut;

		private cbResetDeviceP _cbResetDevice;

		private cbDeviceLoginP _cbDeviceLogin;

		private cbDeviceLogoutP _cbDeviceLogout;

		private cbTEDSReadP _cbTEDSRead;

		private cbLogSetPreferencesP _cbLogSetPreferences;

		private cbLogGetPreferencesP _cbLogGetPreferences;

		private cbLogGetFileNameP _cbLogGetFileName;

		private cbLogGetFileInfoP _cbLogGetFileInfo;

		private cbLogGetSampleInfoP _cbLogGetSampleInfo;

		private cbLogGetAIChannelCountP _cbLogGetAIChannelCount;

		private cbLogGetAIInfoP _cbLogGetAIInfo;

		private cbLogGetCJCInfoP _cbLogGetCJCInfo;

		private cbLogGetDIOInfoP _cbLogGetDIOInfo;

		private cbLogReadTimeTagsP _cbLogReadTimeTags;

		private cbLogReadAIChannelsP _cbLogReadAIChannels;

		private cbLogReadCJCChannelsP _cbLogReadCJCChannels;

		private cbLogReadDIOChannelsP _cbLogReadDIOChannels;

		private cbLogConvertFileP _cbLogConvertFile;

		private cbACalibrateData_noncls_ObsoleteP _cbACalibrateData_noncls_Obsolete;

		private cbAConvertData_noncls_ObsoleteP _cbAConvertData_noncls_Obsolete;

		private cbAConvertPretrigData_noncls_ObsoleteP _cbAConvertPretrigData_noncls_Obsolete;

		private cbAIn_nonclsP _cbAIn_noncls;

		private cbAIn32_nonclsP _cbAIn32_noncls;

		private cbATrig_nonclsP _cbATrig_noncls;

		private cbCFreqIn_nonclsP _cbCFreqIn_noncls;

		private cbCIn_nonclsP _cbCIn_noncls;

		private cbCIn32_nonclsP _cbCIn32_noncls;

		private cbCIn64_nonclsP _cbCIn64_noncls;

		private cbDIn_nonclsP _cbDIn_noncls;

		private cbMemWrite_noncls_ObsoleteP _cbMemWrite_noncls_Obsolete;

		private cbMemRead_noncls_ObsoleteP _cbMemRead_noncls_Obsolete;

		private cbMemReadPretrig_noncls_ObsoleteP _cbMemReadPretrig_noncls_Obsolete;

		private cbFromEngUnits_nonclsP _cbFromEngUnits_noncls;

		private cbFileRead_noncls_ObsoleteP _cbFileRead_noncls_Obsolete;

		private cbWinBufToArray_noncls_ObsoleteP _cbWinBufToArray_noncls_Obsolete;

		private cbWinBufToArray32_noncls_ObsoleteP _cbWinBufToArray32_noncls_Obsolete;

		private cbWinArrayToBuf_noncls_ObsoleteP _cbWinArrayToBuf_noncls_Obsolete;

		private cbACalibrateDataP _cbACalibrateData;

		private cbAConvertDataP _cbAConvertData;

		private cbAConvertPretrigDataP _cbAConvertPretrigData;

		private cbAInScanP _cbAInScan;

		private cbAOutScanP _cbAOutScan;

		private cbAPretrigP _cbAPretrig;

		private cbCStoreOnIntP _cbCStoreOnInt;

		private cbCInScanP _cbCInScan;

		private cbDInScanP _cbDInScan;

		private cbDOutScanP _cbDOutScan;

		private cbWinBufToEngUnitsP _cbWinBufToEngUnits;

		private cbWinBufFromEngUnitsP _cbWinBufFromEngUnits;

		private cbDaqInScanP _cbDaqInScan;

		private cbDaqOutScanP _cbDaqOutScan;

		private cbGetTCValuesP _cbGetTCValues;

		private cbTInScanP _cbTInScan;

		private cbMemReadP _cbMemRead;

		private cbMemWriteP _cbMemWrite;

		private cbMemReadPretrigP _cbMemReadPretrig;

		private cbFileReadP _cbFileRead;

		private cbWinBufToArrayP _cbWinBufToArray;

		private cbWinBufToArray32P _cbWinBufToArray32;

		private cbScaledWinBufToArrayP _cbScaledWinBufToArray;

		private cbWinArrayToBufP _cbWinArrayToBuf;

		private cbScaledWinArrayToBufP _cbScaledWinArrayToBuf;

		private cbWinBufAllocP _cbWinBufAlloc;

		private cbWinBufAlloc32P _cbWinBufAlloc32;

		private cbWinBufAlloc64P _cbWinBufAlloc64;

		private cbScaledWinBufAllocP _cbScaledWinBufAlloc;

		private cbWinBufFreeP _cbWinBufFree;

		private cbACalibrateData_nonclsP _cbACalibrateData_noncls;

		private cbAConvertData_nonclsP _cbAConvertData_noncls;

		private cbAConvertPretrigData_nonclsP _cbAConvertPretrigData_noncls;

		private cbMemWrite_nonclsP _cbMemWrite_noncls;

		private cbMemRead_nonclsP _cbMemRead_noncls;

		private cbMemReadPretrig_nonclsP _cbMemReadPretrig_noncls;

		private cbFileRead_nonclsP _cbFileRead_noncls;

		private cbWinBufToArray_nonclsP _cbWinBufToArray_noncls;

		private cbWinBufToArray32_nonclsP _cbWinBufToArray32_noncls;

		private cbWinArrayToBuf_nonclsP _cbWinArrayToBuf_noncls;

		public static CbwApi Instance
		{
			get
			{
				lock (CbwApi.m_syncRoot)
				{
					if (CbwApi.m_instance == null)
					{
						CbwApi.m_instance = new CbwApi();
					}
				}
				return CbwApi.m_instance;
			}
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllName);

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		private CbwApi()
		{
			if (IntPtr.Size == 8)
			{
				CbwApi.m_cbwDll = CbwApi.LoadLibrary("cbw64.dll");
			}
			else
			{
				CbwApi.m_cbwDll = CbwApi.LoadLibrary("cbw32.dll");
			}
			if (CbwApi.m_cbwDll != IntPtr.Zero)
			{
				this._cbGetRevision = (cbGetRevisionP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetRevision"), typeof(cbGetRevisionP));
				this._cbLoadConfig = (cbLoadConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLoadConfig"), typeof(cbLoadConfigP));
				this._cbSaveConfig = (cbSaveConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSaveConfig"), typeof(cbSaveConfigP));
				this._cbErrHandling = (cbErrHandlingP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbErrHandling"), typeof(cbErrHandlingP));
				this._cbFileGetInfo = (cbFileGetInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileGetInfo"), typeof(cbFileGetInfoP));
				this._cbFileRead_Obsolete = (cbFileRead_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileRead"), typeof(cbFileRead_ObsoleteP));
				this._cbWinBufToArray_Obsolete = (cbWinBufToArray_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray"), typeof(cbWinBufToArray_ObsoleteP));
				this._cbWinBufToArray32_Obsolete = (cbWinBufToArray32_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray32"), typeof(cbWinBufToArray32_ObsoleteP));
				this._cbScaledWinBufToArray_Obsolete = (cbScaledWinBufToArray_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinBufToArray"), typeof(cbScaledWinBufToArray_ObsoleteP));
				this._cbWinArrayToBuf_Obsolete = (cbWinArrayToBuf_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinArrayToBuf"), typeof(cbWinArrayToBuf_ObsoleteP));
				this._cbScaledWinArrayToBuf_Obsolete = (cbScaledWinArrayToBuf_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinArrayToBuf"), typeof(cbScaledWinArrayToBuf_ObsoleteP));
				this._cbWinBufAlloc_Obsolete = (cbWinBufAlloc_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc"), typeof(cbWinBufAlloc_ObsoleteP));
				this._cbWinBufAlloc32_Obsolete = (cbWinBufAlloc32_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc32"), typeof(cbWinBufAlloc32_ObsoleteP));
				this._cbWinBufAlloc64_Obsolete = (cbWinBufAlloc64_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc64"), typeof(cbWinBufAlloc64_ObsoleteP));
				this._cbScaledWinBufAlloc_Obsolete = (cbScaledWinBufAlloc_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinBufAlloc"), typeof(cbScaledWinBufAlloc_ObsoleteP));
				this._cbWinBufFree_Obsolete = (cbWinBufFree_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufFree"), typeof(cbWinBufFree_ObsoleteP));
				this._cbDeclareRevision = (cbDeclareRevisionP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDeclareRevision"), typeof(cbDeclareRevisionP));
				this._cbGetBoardName = (cbGetBoardNameP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetBoardName"), typeof(cbGetBoardNameP));
				this._cbGetErrMsg = (cbGetErrMsgP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetErrMsg"), typeof(cbGetErrMsgP));
				this._cbACalibrateData_Obsolete = (cbACalibrateData_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbACalibrateData"), typeof(cbACalibrateData_ObsoleteP));
				this._cbAConvertData_Obsolete = (cbAConvertData_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertData"), typeof(cbAConvertData_ObsoleteP));
				this._cbAConvertPretrigData_Obsolete = (cbAConvertPretrigData_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertPretrigData"), typeof(cbAConvertPretrigData_ObsoleteP));
				this._cbAIn = (cbAInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAIn"), typeof(cbAInP));
				this._cbAIn32 = (cbAIn32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAIn32"), typeof(cbAIn32P));
				this._cbAInScan_Obsolete = (cbAInScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAInScan"), typeof(cbAInScan_ObsoleteP));
				this._cbALoadQueue = (cbALoadQueueP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbALoadQueue"), typeof(cbALoadQueueP));
				this._cbAOut = (cbAOutP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAOut"), typeof(cbAOutP));
				this._cbAOutScan_Obsolete = (cbAOutScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAOutScan"), typeof(cbAOutScan_ObsoleteP));
				this._cbAPretrig_Obsolete = (cbAPretrig_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAPretrig"), typeof(cbAPretrig_ObsoleteP));
				this._cbATrig = (cbATrigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbATrig"), typeof(cbATrigP));
				this._cbC7266Config = (cbC7266ConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC7266Config"), typeof(cbC7266ConfigP));
				this._cbC8254Config = (cbC8254ConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC8254Config"), typeof(cbC8254ConfigP));
				this._cbC8536Config = (cbC8536ConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC8536Config"), typeof(cbC8536ConfigP));
				this._cbC9513Config = (cbC9513ConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC9513Config"), typeof(cbC9513ConfigP));
				this._cbC8536Init = (cbC8536InitP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC8536Init"), typeof(cbC8536InitP));
				this._cbC9513Init = (cbC9513InitP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbC9513Init"), typeof(cbC9513InitP));
				this._cbCFreqIn = (cbCFreqInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCFreqIn"), typeof(cbCFreqInP));
				this._cbCIn = (cbCInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn"), typeof(cbCInP));
				this._cbCIn32 = (cbCIn32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn32"), typeof(cbCIn32P));
				this._cbCIn64 = (cbCIn64P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn64"), typeof(cbCIn64P));
				this._cbCLoad = (cbCLoadP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCLoad"), typeof(cbCLoadP));
				this._cbCLoad32 = (cbCLoad32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCLoad32"), typeof(cbCLoad32P));
				this._cbCLoad64 = (cbCLoad64P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCLoad64"), typeof(cbCLoad64P));
				this._cbCStatus = (cbCStatusP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCStatus"), typeof(cbCStatusP));
				this._cbCStoreOnInt_Obsolete = (cbCStoreOnInt_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCStoreOnInt"), typeof(cbCStoreOnInt_ObsoleteP));
				this._cbCInScan_Obsolete = (cbCInScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCInScan"), typeof(cbCInScan_ObsoleteP));
				this._cbCConfigScan = (cbCConfigScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCConfigScan"), typeof(cbCConfigScanP));
				this._cbCClear = (cbCClearP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCClear"), typeof(cbCClearP));
				this._cbTimerOutStart = (cbTimerOutStartP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTimerOutStart"), typeof(cbTimerOutStartP));
				this._cbTimerOutStop = (cbTimerOutStopP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTimerOutStop"), typeof(cbTimerOutStopP));
				this._cbPulseOutStart = (cbPulseOutStartP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbPulseOutStart"), typeof(cbPulseOutStartP));
				this._cbPulseOutStop = (cbPulseOutStopP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbPulseOutStop"), typeof(cbPulseOutStopP));
				this._cbDBitIn = (cbDBitInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDBitIn"), typeof(cbDBitInP));
				this._cbDBitOut = (cbDBitOutP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDBitOut"), typeof(cbDBitOutP));
				this._cbDConfigPort = (cbDConfigPortP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDConfigPort"), typeof(cbDConfigPortP));
				this._cbDConfigBit = (cbDConfigBitP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDConfigBit"), typeof(cbDConfigBitP));
				this._cbDIn = (cbDInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDIn"), typeof(cbDInP));
				this._cbDInScan_Obsolete = (cbDInScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDInScan"), typeof(cbDInScan_ObsoleteP));
				this._cbDOut = (cbDOutP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDOut"), typeof(cbDOutP));
				this._cbDOutScan_Obsolete = (cbDOutScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDOutScan"), typeof(cbDOutScan_ObsoleteP));
				this._cbFileAInScan = (cbFileAInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileAInScan"), typeof(cbFileAInScanP));
				this._cbFilePretrig = (cbFilePretrigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFilePretrig"), typeof(cbFilePretrigP));
				this._cbFlashLED = (cbFlashLEDP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFlashLED"), typeof(cbFlashLEDP));
				this._cbGetIOStatus = (cbGetIOStatusP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetIOStatus"), typeof(cbGetIOStatusP));
				this._cbRS485 = (cbRS485P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbRS485"), typeof(cbRS485P));
				this._cbStopIOBackground = (cbStopIOBackgroundP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbStopIOBackground"), typeof(cbStopIOBackgroundP));
				this._cbTIn = (cbTInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTIn"), typeof(cbTInP));
				this._cbTInScan_Obsolete = (cbTInScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTInScan"), typeof(cbTInScan_ObsoleteP));
				this._cbMemReset = (cbMemResetP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemReset"), typeof(cbMemResetP));
				this._cbMemRead_Obsolete = (cbMemRead_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemRead"), typeof(cbMemRead_ObsoleteP));
				this._cbMemWrite_Obsolete = (cbMemWrite_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemWrite"), typeof(cbMemWrite_ObsoleteP));
				this._cbMemReadPretrig_Obsolete = (cbMemReadPretrig_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemReadPretrig"), typeof(cbMemReadPretrig_ObsoleteP));
				this._cbInByte = (cbInByteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbInByte"), typeof(cbInByteP));
				this._cbOutByte = (cbOutByteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbOutByte"), typeof(cbOutByteP));
				this._cbInWord = (cbInWordP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbInWord"), typeof(cbInWordP));
				this._cbOutWord = (cbOutWordP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbOutWord"), typeof(cbOutWordP));
				this._cbGetConfigString = (cbGetConfigStringP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetConfigString"), typeof(cbGetConfigStringP));
				this._cbGetConfig = (cbGetConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetConfig"), typeof(cbGetConfigP));
				this._cbSetConfigString = (cbSetConfigStringP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSetConfigString"), typeof(cbSetConfigStringP));
				this._cbSetConfig = (cbSetConfigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSetConfig"), typeof(cbSetConfigP));
				this._cbToEngUnits = (cbToEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbToEngUnits"), typeof(cbToEngUnitsP));
				this._cbToEngUnits32 = (cbToEngUnits32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbToEngUnits32"), typeof(cbToEngUnits32P));
				this._cbFromEngUnits = (cbFromEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFromEngUnits"), typeof(cbFromEngUnitsP));
				this._cbSetTrigger = (cbSetTriggerP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSetTrigger"), typeof(cbSetTriggerP));
				this._cbEnableEvent_noncls = (cbEnableEvent_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbEnableEvent"), typeof(cbEnableEvent_nonclsP));
				this._cbEnableEvent = (cbEnableEventP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbEnableEvent"), typeof(cbEnableEventP));
				this._cbDisableEvent = (cbDisableEventP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDisableEvent"), typeof(cbDisableEventP));
				this._cbSelectSignal = (cbSelectSignalP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSelectSignal"), typeof(cbSelectSignalP));
				this._cbGetSignal = (cbGetSignalP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetSignal"), typeof(cbGetSignalP));
				this._cbSetCalCoeff = (cbSetCalCoeffP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbSetCalCoeff"), typeof(cbSetCalCoeffP));
				this._cbGetCalCoeff = (cbGetCalCoeffP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetCalCoeff"), typeof(cbGetCalCoeffP));
				this._cbWinBufToEngUnits_Obsolete = (cbWinBufToEngUnits_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToEngUnits"), typeof(cbWinBufToEngUnits_ObsoleteP));
				this._cbWinBufFromEngUnits_Obsolete = (cbWinBufFromEngUnits_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufFromEngUnits"), typeof(cbWinBufFromEngUnits_ObsoleteP));
				this._cbDaqInScan_Obsolete = (cbDaqInScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqInScan"), typeof(cbDaqInScan_ObsoleteP));
				this._cbDaqSetTrigger = (cbDaqSetTriggerP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqSetTrigger"), typeof(cbDaqSetTriggerP));
				this._cbDaqSetSetpoints = (cbDaqSetSetpointsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqSetSetpoints"), typeof(cbDaqSetSetpointsP));
				this._cbDaqOutScan_Obsolete = (cbDaqOutScan_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqOutScan"), typeof(cbDaqOutScan_ObsoleteP));
				this._cbGetTCValues_Obsolete = (cbGetTCValues_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetTCValues"), typeof(cbGetTCValues_ObsoleteP));
				this._cbVIn = (cbVInP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbVIn"), typeof(cbVInP));
				this._cbVIn32 = (cbVIn32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbVIn32"), typeof(cbVIn32P));
				this._cbVOut = (cbVOutP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbVOut"), typeof(cbVOutP));
				this._cbResetDevice = (cbResetDeviceP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbResetDevice"), typeof(cbResetDeviceP));
				this._cbDeviceLogin = (cbDeviceLoginP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDeviceLogin"), typeof(cbDeviceLoginP));
				this._cbDeviceLogout = (cbDeviceLogoutP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDeviceLogout"), typeof(cbDeviceLogoutP));
				this._cbTEDSRead = (cbTEDSReadP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTEDSRead"), typeof(cbTEDSReadP));
				this._cbLogSetPreferences = (cbLogSetPreferencesP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogSetPreferences"), typeof(cbLogSetPreferencesP));
				this._cbLogGetPreferences = (cbLogGetPreferencesP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetPreferences"), typeof(cbLogGetPreferencesP));
				this._cbLogGetFileName = (cbLogGetFileNameP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetFileName"), typeof(cbLogGetFileNameP));
				this._cbLogGetFileInfo = (cbLogGetFileInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetFileInfo"), typeof(cbLogGetFileInfoP));
				this._cbLogGetSampleInfo = (cbLogGetSampleInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetSampleInfo"), typeof(cbLogGetSampleInfoP));
				this._cbLogGetAIChannelCount = (cbLogGetAIChannelCountP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetAIChannelCount"), typeof(cbLogGetAIChannelCountP));
				this._cbLogGetAIInfo = (cbLogGetAIInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetAIInfo"), typeof(cbLogGetAIInfoP));
				this._cbLogGetCJCInfo = (cbLogGetCJCInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetCJCInfo"), typeof(cbLogGetCJCInfoP));
				this._cbLogGetDIOInfo = (cbLogGetDIOInfoP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogGetDIOInfo"), typeof(cbLogGetDIOInfoP));
				this._cbLogReadTimeTags = (cbLogReadTimeTagsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogReadTimeTags"), typeof(cbLogReadTimeTagsP));
				this._cbLogReadAIChannels = (cbLogReadAIChannelsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogReadAIChannels"), typeof(cbLogReadAIChannelsP));
				this._cbLogReadCJCChannels = (cbLogReadCJCChannelsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogReadCJCChannels"), typeof(cbLogReadCJCChannelsP));
				this._cbLogReadDIOChannels = (cbLogReadDIOChannelsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogReadDIOChannels"), typeof(cbLogReadDIOChannelsP));
				this._cbLogConvertFile = (cbLogConvertFileP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbLogConvertFile"), typeof(cbLogConvertFileP));
				this._cbACalibrateData_noncls_Obsolete = (cbACalibrateData_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbACalibrateData"), typeof(cbACalibrateData_noncls_ObsoleteP));
				this._cbAConvertData_noncls_Obsolete = (cbAConvertData_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertData"), typeof(cbAConvertData_noncls_ObsoleteP));
				this._cbAConvertPretrigData_noncls_Obsolete = (cbAConvertPretrigData_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertPretrigData"), typeof(cbAConvertPretrigData_noncls_ObsoleteP));
				this._cbAIn_noncls = (cbAIn_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAIn"), typeof(cbAIn_nonclsP));
				this._cbAIn32_noncls = (cbAIn32_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAIn32"), typeof(cbAIn32_nonclsP));
				this._cbATrig_noncls = (cbATrig_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbATrig"), typeof(cbATrig_nonclsP));
				this._cbCFreqIn_noncls = (cbCFreqIn_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCFreqIn"), typeof(cbCFreqIn_nonclsP));
				this._cbCIn_noncls = (cbCIn_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn"), typeof(cbCIn_nonclsP));
				this._cbCIn32_noncls = (cbCIn32_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn32"), typeof(cbCIn32_nonclsP));
				this._cbCIn64_noncls = (cbCIn64_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCIn64"), typeof(cbCIn64_nonclsP));
				this._cbDIn_noncls = (cbDIn_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDIn"), typeof(cbDIn_nonclsP));
				this._cbMemWrite_noncls_Obsolete = (cbMemWrite_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemWrite"), typeof(cbMemWrite_noncls_ObsoleteP));
				this._cbMemRead_noncls_Obsolete = (cbMemRead_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemRead"), typeof(cbMemRead_noncls_ObsoleteP));
				this._cbMemReadPretrig_noncls_Obsolete = (cbMemReadPretrig_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemReadPretrig"), typeof(cbMemReadPretrig_noncls_ObsoleteP));
				this._cbFromEngUnits_noncls = (cbFromEngUnits_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFromEngUnits"), typeof(cbFromEngUnits_nonclsP));
				this._cbFileRead_noncls_Obsolete = (cbFileRead_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileRead"), typeof(cbFileRead_noncls_ObsoleteP));
				this._cbWinBufToArray_noncls_Obsolete = (cbWinBufToArray_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray"), typeof(cbWinBufToArray_noncls_ObsoleteP));
				this._cbWinBufToArray32_noncls_Obsolete = (cbWinBufToArray32_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray32"), typeof(cbWinBufToArray32_noncls_ObsoleteP));
				this._cbWinArrayToBuf_noncls_Obsolete = (cbWinArrayToBuf_noncls_ObsoleteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinArrayToBuf"), typeof(cbWinArrayToBuf_noncls_ObsoleteP));
				this._cbACalibrateData = (cbACalibrateDataP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbACalibrateData"), typeof(cbACalibrateDataP));
				this._cbAConvertData = (cbAConvertDataP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertData"), typeof(cbAConvertDataP));
				this._cbAConvertPretrigData = (cbAConvertPretrigDataP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertPretrigData"), typeof(cbAConvertPretrigDataP));
				this._cbAInScan = (cbAInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAInScan"), typeof(cbAInScanP));
				this._cbAOutScan = (cbAOutScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAOutScan"), typeof(cbAOutScanP));
				this._cbAPretrig = (cbAPretrigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAPretrig"), typeof(cbAPretrigP));
				this._cbCStoreOnInt = (cbCStoreOnIntP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCStoreOnInt"), typeof(cbCStoreOnIntP));
				this._cbCInScan = (cbCInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbCInScan"), typeof(cbCInScanP));
				this._cbDInScan = (cbDInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDInScan"), typeof(cbDInScanP));
				this._cbDOutScan = (cbDOutScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDOutScan"), typeof(cbDOutScanP));
				this._cbWinBufToEngUnits = (cbWinBufToEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToEngUnits"), typeof(cbWinBufToEngUnitsP));
				this._cbWinBufFromEngUnits = (cbWinBufFromEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufFromEngUnits"), typeof(cbWinBufFromEngUnitsP));
				this._cbDaqInScan = (cbDaqInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqInScan"), typeof(cbDaqInScanP));
				this._cbDaqOutScan = (cbDaqOutScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbDaqOutScan"), typeof(cbDaqOutScanP));
				this._cbGetTCValues = (cbGetTCValuesP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbGetTCValues"), typeof(cbGetTCValuesP));
				this._cbTInScan = (cbTInScanP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbTInScan"), typeof(cbTInScanP));
				this._cbToEngUnits = (cbToEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbToEngUnits"), typeof(cbToEngUnitsP));
				this._cbToEngUnits32 = (cbToEngUnits32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbToEngUnits32"), typeof(cbToEngUnits32P));
				this._cbFromEngUnits = (cbFromEngUnitsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFromEngUnits"), typeof(cbFromEngUnitsP));
				this._cbWinBufToArray = (cbWinBufToArrayP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray"), typeof(cbWinBufToArrayP));
				this._cbWinBufToArray32 = (cbWinBufToArray32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray32"), typeof(cbWinBufToArray32P));
				this._cbScaledWinBufToArray = (cbScaledWinBufToArrayP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinBufToArray"), typeof(cbScaledWinBufToArrayP));
				this._cbWinArrayToBuf = (cbWinArrayToBufP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinArrayToBuf"), typeof(cbWinArrayToBufP));
				this._cbScaledWinArrayToBuf = (cbScaledWinArrayToBufP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinArrayToBuf"), typeof(cbScaledWinArrayToBufP));
				this._cbWinBufAlloc = (cbWinBufAllocP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc"), typeof(cbWinBufAllocP));
				this._cbWinBufAlloc32 = (cbWinBufAlloc32P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc32"), typeof(cbWinBufAlloc32P));
				this._cbWinBufAlloc64 = (cbWinBufAlloc64P)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufAlloc64"), typeof(cbWinBufAlloc64P));
				this._cbScaledWinBufAlloc = (cbScaledWinBufAllocP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbScaledWinBufAlloc"), typeof(cbScaledWinBufAllocP));
				this._cbWinBufFree = (cbWinBufFreeP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufFree"), typeof(cbWinBufFreeP));
				this._cbMemRead = (cbMemReadP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemRead"), typeof(cbMemReadP));
				this._cbMemWrite = (cbMemWriteP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemWrite"), typeof(cbMemWriteP));
				this._cbMemReadPretrig = (cbMemReadPretrigP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemReadPretrig"), typeof(cbMemReadPretrigP));
				this._cbFileRead = (cbFileReadP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileRead"), typeof(cbFileReadP));
				this._cbACalibrateData_noncls = (cbACalibrateData_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbACalibrateData"), typeof(cbACalibrateData_nonclsP));
				this._cbAConvertData_noncls = (cbAConvertData_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertData"), typeof(cbAConvertData_nonclsP));
				this._cbAConvertPretrigData_noncls = (cbAConvertPretrigData_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbAConvertPretrigData"), typeof(cbAConvertPretrigData_nonclsP));
				this._cbMemWrite_noncls = (cbMemWrite_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemWrite"), typeof(cbMemWrite_nonclsP));
				this._cbMemRead_noncls = (cbMemRead_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemRead"), typeof(cbMemRead_nonclsP));
				this._cbMemReadPretrig_noncls = (cbMemReadPretrig_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbMemReadPretrig"), typeof(cbMemReadPretrig_nonclsP));
				this._cbFileRead_noncls = (cbFileRead_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbFileRead"), typeof(cbFileRead_nonclsP));
				this._cbWinBufToArray_noncls = (cbWinBufToArray_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray"), typeof(cbWinBufToArray_nonclsP));
				this._cbWinBufToArray32_noncls = (cbWinBufToArray32_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinBufToArray32"), typeof(cbWinBufToArray32_nonclsP));
				this._cbWinArrayToBuf_noncls = (cbWinArrayToBuf_nonclsP)Marshal.GetDelegateForFunctionPointer(CbwApi.GetProcAddress(CbwApi.m_cbwDll, "cbWinArrayToBuf"), typeof(cbWinArrayToBuf_nonclsP));
			}
		}

		public int cbGetRevision(out float revNum, out float vxDRevNum)
		{
			return this._cbGetRevision(out revNum, out vxDRevNum);
		}

		public int cbLoadConfig(string cfgFileName)
		{
			return this._cbLoadConfig(cfgFileName);
		}

		public int cbSaveConfig(string cfgFileName)
		{
			return this._cbSaveConfig(cfgFileName);
		}

		public int cbErrHandling(int errorReporting, int errorHandling)
		{
			return this._cbErrHandling(errorReporting, errorHandling);
		}

		public int cbFileGetInfo(string fileName, out short lowChan, out short highChan, out int pretrigCount, out int totalCount, out int rate, out int gain)
		{
			return this._cbFileGetInfo(fileName, out lowChan, out highChan, out pretrigCount, out totalCount, out rate, out gain);
		}

		public int cbFileRead_Obsolete(string fileName, int firstPoint, ref int numPoints, out short dataBuffer)
		{
			return this._cbFileRead_Obsolete(fileName, firstPoint, ref numPoints, out dataBuffer);
		}

		public int cbWinBufToArray_Obsolete(int memHandle, out short dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray_Obsolete(memHandle, out dataArray, startPoint, count);
		}

		public int cbWinBufToArray32_Obsolete(int memHandle, out int dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray32_Obsolete(memHandle, out dataArray, startPoint, count);
		}

		public int cbScaledWinBufToArray_Obsolete(int memHandle, out double dataArray, int startPoint, int count)
		{
			return this.cbScaledWinBufToArray_Obsolete(memHandle, out dataArray, startPoint, count);
		}

		public int cbWinArrayToBuf_Obsolete(ref short DataArray, int MemHandle, int StartPt, int Count)
		{
			return this._cbWinArrayToBuf_Obsolete(ref DataArray, MemHandle, StartPt, Count);
		}

		public int cbScaledWinArrayToBuf_Obsolete(ref double DataArray, int MemHandle, int StartPt, int Count)
		{
			return this._cbScaledWinArrayToBuf_Obsolete(ref DataArray, MemHandle, StartPt, Count);
		}

		public int cbWinBufAlloc_Obsolete(int NumPoints)
		{
			return this._cbWinBufAlloc_Obsolete(NumPoints);
		}

		public int cbWinBufAlloc32_Obsolete(int NumPoints)
		{
			return this._cbWinBufAlloc32_Obsolete(NumPoints);
		}

		public int cbWinBufAlloc64_Obsolete(int NumPoints)
		{
			return this._cbWinBufAlloc64_Obsolete(NumPoints);
		}

		public int cbScaledWinBufAlloc_Obsolete(int NumPoints)
		{
			return this._cbScaledWinBufAlloc_Obsolete(NumPoints);
		}

		public int cbWinBufFree_Obsolete(int MemHandle)
		{
			return this._cbWinBufFree_Obsolete(MemHandle);
		}

		public int cbDeclareRevision(ref float RevNum)
		{
			return this._cbDeclareRevision(ref RevNum);
		}

		public int cbGetBoardName(int BoardNum, ref string BoardName)
		{
			return this._cbGetBoardName(BoardNum, ref BoardName);
		}

		public int cbGetErrMsg(int ErrCode, ref string ErrMsg)
		{
			return this._cbGetErrMsg(ErrCode, ref ErrMsg);
		}

		public int cbACalibrateData_Obsolete(int BoardNum, int NumPoints, int Gain, ref short ADData)
		{
			return this._cbACalibrateData_Obsolete(BoardNum, NumPoints, Gain, ref ADData);
		}

		public int cbAConvertData_Obsolete(int BoardNum, int NumPoints, ref short ADData, out short ChanTags)
		{
			return this._cbAConvertData_Obsolete(BoardNum, NumPoints, ref ADData, out ChanTags);
		}

		public int cbAConvertPretrigData_Obsolete(int BoardNum, int PreTrigCount, int TotalCount, ref short ADData, out short ChanTags)
		{
			return this._cbAConvertPretrigData_Obsolete(BoardNum, PreTrigCount, TotalCount, ref ADData, out ChanTags);
		}

		public int cbAIn(int BoardNum, int Chan, int Gain, out short DataValue)
		{
			return this._cbAIn(BoardNum, Chan, Gain, out DataValue);
		}

		public int cbAIn32(int BoardNum, int Chan, int Gain, out int DataValue, int Options)
		{
			return this._cbAIn32(BoardNum, Chan, Gain, out DataValue, Options);
		}

		public int cbAInScan_Obsolete(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, int MemHandle, int Options)
		{
			return this._cbAInScan_Obsolete(BoardNum, LowChan, HighChan, Count, ref Rate, Gain, MemHandle, Options);
		}

		public int cbALoadQueue(int BoardNum, short[] ChanArray, short[] GainArray, int Count)
		{
			return this._cbALoadQueue(BoardNum, ChanArray, GainArray, Count);
		}

		public int cbAOut(int BoardNum, int Chan, int Gain, ushort DataValue)
		{
			return this._cbAOut(BoardNum, Chan, Gain, DataValue);
		}

		public int cbAOutScan_Obsolete(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, int MemHandle, int Options)
		{
			return this._cbAOutScan_Obsolete(BoardNum, LowChan, HighChan, Count, ref Rate, Gain, MemHandle, Options);
		}

		public int cbAPretrig_Obsolete(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, int MemHandle, int Options)
		{
			return this._cbAPretrig_Obsolete(BoardNum, LowChan, HighChan, ref PreTrigCount, ref TotalCount, ref Rate, Gain, MemHandle, Options);
		}

		public int cbATrig(int BoardNum, int Chan, int TrigType, short TrigValue, int Gain, out short DataValue)
		{
			return this._cbATrig(BoardNum, Chan, TrigType, TrigValue, Gain, out DataValue);
		}

		public int cbC7266Config(int BoardNum, int CounterNum, int Quadrature, int CountingMode, int DataEncoding, int IndexMode, int InvertIndex, int FlagPins, int GateEnable)
		{
			return this._cbC7266Config(BoardNum, CounterNum, Quadrature, CountingMode, DataEncoding, IndexMode, InvertIndex, FlagPins, GateEnable);
		}

		public int cbC8254Config(int BoardNum, int CounterNum, int Config)
		{
			return this._cbC8254Config(BoardNum, CounterNum, Config);
		}

		public int cbC8536Config(int BoardNum, int CounterNum, int OutputControl, int RecycleMode, int TrigType)
		{
			return this._cbC8536Config(BoardNum, CounterNum, OutputControl, RecycleMode, TrigType);
		}

		public int cbC9513Config(int BoardNum, int CounterNum, int GateControl, int CounterEdge, int CountSource, int SpecialGate, int Reload, int RecycleMode, int BCDMode, int CountDirection, int OutputControl)
		{
			return this._cbC9513Config(BoardNum, CounterNum, GateControl, CounterEdge, CountSource, SpecialGate, Reload, RecycleMode, BCDMode, CountDirection, OutputControl);
		}

		public int cbC8536Init(int BoardNum, int ChipNum, int Ctr1Output)
		{
			return this._cbC8536Init(BoardNum, ChipNum, Ctr1Output);
		}

		public int cbC9513Init(int BoardNum, int ChipNum, int FOutDivider, int FOutSource, int Compare1, int Compare2, int TimeOfDay)
		{
			return this._cbC9513Init(BoardNum, ChipNum, FOutDivider, FOutSource, Compare1, Compare2, TimeOfDay);
		}

		public int cbCFreqIn(int BoardNum, int SigSource, int GateInterval, out short Count, out int Freq)
		{
			return this._cbCFreqIn(BoardNum, SigSource, GateInterval, out Count, out Freq);
		}

		public int cbCIn(int BoardNum, int CounterNum, out short Count)
		{
			return this._cbCIn(BoardNum, CounterNum, out Count);
		}

		public int cbCIn32(int BoardNum, int CounterNum, out int Count)
		{
			return this._cbCIn32(BoardNum, CounterNum, out Count);
		}

		public int cbCIn64(int BoardNum, int CounterNum, out long Count)
		{
			return this._cbCIn64(BoardNum, CounterNum, out Count);
		}

		public int cbCLoad(int BoardNum, int RegNum, uint LoadValue)
		{
			return this._cbCLoad(BoardNum, RegNum, LoadValue);
		}

		public int cbCLoad32(int BoardNum, int RegNum, uint LoadValue)
		{
			return this._cbCLoad32(BoardNum, RegNum, LoadValue);
		}

		public int cbCLoad64(int BoardNum, int RegNum, ulong LoadValue)
		{
			return this._cbCLoad64(BoardNum, RegNum, LoadValue);
		}

		public int cbCStatus(int BoardNum, int CounterNum, out uint StatusBits)
		{
			return this._cbCStatus(BoardNum, CounterNum, out StatusBits);
		}

		public int cbCStoreOnInt_Obsolete(int BoardNum, int IntCount, ref CounterControl CntrControl, int MemHandle)
		{
			return this._cbCStoreOnInt_Obsolete(BoardNum, IntCount, ref CntrControl, MemHandle);
		}

		public int cbCInScan_Obsolete(int BoardNum, int FirstCtr, int LastCtr, int Count, ref int Rate, int MemHandle, uint Options)
		{
			return this._cbCInScan_Obsolete(BoardNum, FirstCtr, LastCtr, Count, ref Rate, MemHandle, Options);
		}

		public int cbCConfigScan_Obsolete(int BoardNum, int CounterNum, uint Mode, uint DebounceTime, uint DebounceTrigger, uint EdgeDetection, uint TickSize, uint MapCounter)
		{
			return this._cbCConfigScan(BoardNum, CounterNum, Mode, DebounceTime, DebounceTrigger, EdgeDetection, TickSize, MapCounter);
		}

		public int cbCClear(int BoardNum, int CounterNum)
		{
			return this._cbCClear(BoardNum, CounterNum);
		}

		public int cbTimerOutStart(int BoardNum, int TimerNum, ref double Frequency)
		{
			return this._cbTimerOutStart(BoardNum, TimerNum, ref Frequency);
		}

		public int cbTimerOutStop(int BoardNum, int TimerNum)
		{
			return this._cbTimerOutStop(BoardNum, TimerNum);
		}

		public int cbPulseOutStart(int BoardNum, int TimerNum, ref double Frequency, ref double DutyCycle, uint PulseCount, ref double InitialDelay, int IdleState, int Options)
		{
			return this._cbPulseOutStart(BoardNum, TimerNum, ref Frequency, ref DutyCycle, PulseCount, ref InitialDelay, IdleState, Options);
		}

		public int cbPulseOutStop(int BoardNum, int TimerNum)
		{
			return this._cbPulseOutStop(BoardNum, TimerNum);
		}

		public int cbDBitIn(int BoardNum, int PortType, int BitNum, out ushort BitValue)
		{
			return this._cbDBitIn(BoardNum, PortType, BitNum, out BitValue);
		}

		public int cbDBitOut(int BoardNum, int PortType, int BitNum, ushort BitValue)
		{
			return this._cbDBitOut(BoardNum, PortType, BitNum, BitValue);
		}

		public int cbDConfigPort(int BoardNum, int PortNum, int Direction)
		{
			return this._cbDConfigPort(BoardNum, PortNum, Direction);
		}

		public int cbDConfigBit(int BoardNum, int PortNum, int BitNum, int Direction)
		{
			return this._cbDConfigBit(BoardNum, PortNum, BitNum, Direction);
		}

		public int cbDIn(int BoardNum, int PortNum, out short DataValue)
		{
			return this._cbDIn(BoardNum, PortNum, out DataValue);
		}

		public int cbDInScan_Obsolete(int BoardNum, int PortNum, int Count, ref int Rate, int MemHandle, int Options)
		{
			return this._cbDInScan_Obsolete(BoardNum, PortNum, Count, ref Rate, MemHandle, Options);
		}

		public int cbDOut(int BoardNum, int PortNum, ushort DataValue)
		{
			return this._cbDOut(BoardNum, PortNum, DataValue);
		}

		public int cbDOutScan_Obsolete(int BoardNum, int PortNum, int Count, ref int Rate, int MemHandle, int Options)
		{
			return this._cbDOutScan_Obsolete(BoardNum, PortNum, Count, ref Rate, MemHandle, Options);
		}

		public int cbFileAInScan(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, string FileName, int Options)
		{
			return this._cbFileAInScan(BoardNum, LowChan, HighChan, Count, ref Rate, Gain, FileName, Options);
		}

		public int cbFilePretrig(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, string FileName, int Options)
		{
			return this._cbFilePretrig(BoardNum, LowChan, HighChan, ref PreTrigCount, ref TotalCount, ref Rate, Gain, FileName, Options);
		}

		public int cbFlashLED(int BoardNum)
		{
			return this._cbFlashLED(BoardNum);
		}

		public int cbGetIOStatus(int BoardNum, out short Status, out int CurCount, out int CurIndex, int FunctionType)
		{
			return this._cbGetIOStatus(BoardNum, out Status, out CurCount, out CurIndex, FunctionType);
		}

		public int cbRS485(int BoardNum, int Transmit, int Receive)
		{
			return this._cbRS485(BoardNum, Transmit, Receive);
		}

		public int cbStopIOBackground(int BoardNum, int FunctionType)
		{
			return this._cbStopIOBackground(BoardNum, FunctionType);
		}

		public int cbTIn(int BoardNum, int Chan, int Scale, out float TempValue, int Options)
		{
			return this._cbTIn(BoardNum, Chan, Scale, out TempValue, Options);
		}

		public int cbTInScan_Obsolete(int BoardNum, int LowChan, int HighChan, int Scale, out float DataBuffer, int Options)
		{
			return this._cbTInScan_Obsolete(BoardNum, LowChan, HighChan, Scale, out DataBuffer, Options);
		}

		public int cbMemSetDTMode(int BoardNum, int Mode)
		{
			return 1;
		}

		public int cbMemReset(int BoardNum)
		{
			return this._cbMemReset(BoardNum);
		}

		public int cbMemRead_Obsolete(int BoardNum, out short DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemRead_Obsolete(BoardNum, out DataBuffer, FirstPoint, Count);
		}

		public int cbMemWrite_Obsolete(int BoardNum, ref short DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemWrite_Obsolete(BoardNum, ref DataBuffer, FirstPoint, Count);
		}

		public int cbMemReadPretrig_Obsolete(int BoardNum, out short DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemReadPretrig_Obsolete(BoardNum, out DataBuffer, FirstPoint, Count);
		}

		public int cbInByte(int BoardNum, int PortNum)
		{
			return this._cbInByte(BoardNum, PortNum);
		}

		public int cbOutByte(int BoardNum, int PortNum, int PortVal)
		{
			return this._cbOutByte(BoardNum, PortNum, PortVal);
		}

		public int cbInWord(int BoardNum, int PortNum)
		{
			return this._cbInWord(BoardNum, PortNum);
		}

		public int cbOutWord(int BoardNum, int PortNum, int PortVal)
		{
			return this._cbOutWord(BoardNum, PortNum, PortVal);
		}

		public int cbGetConfigString(int InfoType, int BoardNum, int DevNum, int ConfigItem, StringBuilder ConfigVal, ref int MaxConfigLen)
		{
			return this._cbGetConfigString(InfoType, BoardNum, DevNum, ConfigItem, ConfigVal, ref MaxConfigLen);
		}

		public int cbGetConfig(int InfoType, int BoardNum, int DevNum, int ConfigItem, out int ConfigVal)
		{
			return this._cbGetConfig(InfoType, BoardNum, DevNum, ConfigItem, out ConfigVal);
		}

		public int cbSetConfigString(int InfoType, int BoardNum, int DevNum, int ConfigItem, ref string ConfigVal, ref int MaxConfigLen)
		{
			return this._cbSetConfigString(InfoType, BoardNum, DevNum, ConfigItem, ref ConfigVal, ref MaxConfigLen);
		}

		public int cbSetConfig(int InfoType, int BoardNum, int DevNum, int ConfigItem, int ConfigVal)
		{
			return this._cbSetConfig(InfoType, BoardNum, DevNum, ConfigItem, ConfigVal);
		}

		public int cbToEngUnits(int BoardNum, int Range, ushort DataVal, out float EngUnits)
		{
			return this._cbToEngUnits(BoardNum, Range, DataVal, out EngUnits);
		}

		public int cbToEngUnits32(int BoardNum, int Range, uint DataVal, out double EngUnits)
		{
			return this._cbToEngUnits32(BoardNum, Range, DataVal, out EngUnits);
		}

		public int cbFromEngUnits(int BoardNum, int Range, float EngUnits, out short DataVal)
		{
			return this._cbFromEngUnits(BoardNum, Range, EngUnits, out DataVal);
		}

		public int cbSetTrigger(int BoardNum, int TrigType, ushort LowThreshold, ushort HighThreshold)
		{
			return this._cbSetTrigger(BoardNum, TrigType, LowThreshold, HighThreshold);
		}

		public int cbEnableEvent_noncls(int BoardNum, uint EventType, uint Count, EventCallback CallbackFunc, IntPtr UserData)
		{
			return this._cbEnableEvent_noncls(BoardNum, EventType, Count, CallbackFunc, UserData);
		}

		public int cbEnableEvent(int BoardNum, uint EventType, uint Count, CallbackFunction CallbackFunc, IntPtr UserData)
		{
			return this._cbEnableEvent(BoardNum, EventType, Count, CallbackFunc, UserData);
		}

		public int cbDisableEvent(int BoardNum, uint EventType)
		{
			return this._cbDisableEvent(BoardNum, EventType);
		}

		public int cbSelectSignal(int BoardNum, int Direction, int Signal, int Connection, int Polarity)
		{
			return this._cbSelectSignal(BoardNum, Direction, Signal, Connection, Polarity);
		}

		public int cbGetSignal(int BoardNum, int Direction, int Signal, int Index, out int Connection, out int Polarity)
		{
			return this._cbGetSignal(BoardNum, Direction, Signal, Index, out Connection, out Polarity);
		}

		public int cbSetCalCoeff(int BoardNum, int FunctionType, int Channel, int Range, int Item, int Value, int Store)
		{
			return this._cbSetCalCoeff(BoardNum, FunctionType, Channel, Range, Item, Value, Store);
		}

		public int cbGetCalCoeff(int BoardNum, int FunctionType, int Channel, int Range, int Item, out int Value)
		{
			return this._cbGetCalCoeff(BoardNum, FunctionType, Channel, Range, Item, out Value);
		}

		public int cbWinBufToEngUnits_Obsolete(int BoardNum, short[] GainArray, int GainCount, int MemHandle, out float EngUnits, int FirstPoint, int Count)
		{
			return this._cbWinBufToEngUnits_Obsolete(BoardNum, GainArray, GainCount, MemHandle, out EngUnits, FirstPoint, Count);
		}

		public int cbWinBufFromEngUnits_Obsolete(int BoardNum, short[] GainArray, int GainCount, ref float EngUnits, int MemHandle, int FirstPoint, int Count)
		{
			return this._cbWinBufFromEngUnits_Obsolete(BoardNum, GainArray, GainCount, ref EngUnits, MemHandle, FirstPoint, Count);
		}

		public int cbDaqInScan_Obsolete(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, ref int PretrigCount, ref int TotalCount, int MemHandle, int Options)
		{
			return this._cbDaqInScan_Obsolete(BoardNum, ChanArray, ChanTypeArray, GainArray, ChanCount, ref Rate, ref PretrigCount, ref TotalCount, MemHandle, Options);
		}

		public int cbDaqSetTrigger(int BoardNum, int TrigSource, int TrigSense, int TrigChan, int ChanType, int Gain, float Level, float Variance, int TrigEvent)
		{
			return this._cbDaqSetTrigger(BoardNum, TrigSource, TrigSense, TrigChan, ChanType, Gain, Level, Variance, TrigEvent);
		}

		public int cbDaqSetSetpoints(int BoardNum, float[] LimitAArray, float[] LimitBArray, float[] reserved, int[] SetpointFlagsArray, int[] SetpointOutputArray, float[] Output1Array, float[] Output2Array, float[] OutputMask1Array, float[] OutputMask2Array, int SetpointCount)
		{
			return this._cbDaqSetSetpoints(BoardNum, LimitAArray, LimitBArray, reserved, SetpointFlagsArray, SetpointOutputArray, Output1Array, Output2Array, OutputMask1Array, OutputMask2Array, SetpointCount);
		}

		public int cbDaqOutScan_Obsolete(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, int Count, int MemHandle, int Options)
		{
			return this._cbDaqOutScan_Obsolete(BoardNum, ChanArray, ChanTypeArray, GainArray, ChanCount, ref Rate, Count, MemHandle, Options);
		}

		public int cbGetTCValues_Obsolete(int BoardNum, short[] ChanArray, short[] ChanTypeArray, int ChanCount, int MemHandle, int FirstPoint, int Count, int Scale, out float TempValArray)
		{
			return this._cbGetTCValues_Obsolete(BoardNum, ChanArray, ChanTypeArray, ChanCount, MemHandle, FirstPoint, Count, Scale, out TempValArray);
		}

		public int cbVIn(int BoardNum, int Chan, int Gain, out float DataValue, int Options)
		{
			return this._cbVIn(BoardNum, Chan, Gain, out DataValue, Options);
		}

		public int cbVIn32(int BoardNum, int Chan, int Gain, out double DataValue, int Options)
		{
			return this._cbVIn32(BoardNum, Chan, Gain, out DataValue, Options);
		}

		public int cbVOut(int BoardNum, int Chan, int Gain, float DataValue, int Options)
		{
			return this._cbVOut(BoardNum, Chan, Gain, DataValue, Options);
		}

		public int cbResetDevice(int BoardNum)
		{
			return this._cbResetDevice(BoardNum);
		}

		public int cbDeviceLogin(int BoardNum, [MarshalAs(UnmanagedType.VBByRefStr)] ref string AccountName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Password)
		{
			return this._cbDeviceLogin(BoardNum, ref AccountName, ref Password);
		}

		public int cbDeviceLogout(int BoardNum)
		{
			return this._cbDeviceLogout(BoardNum);
		}

		public int cbTEDSRead(int BoardNum, int Chan, byte[] DataBuffer, ref int Count, int Options)
		{
			return this._cbTEDSRead(BoardNum, Chan, DataBuffer, ref Count, Options);
		}

		public int cbLogSetPreferences(TimeFormat timeFormat, TimeZone timeZone, TempScale units)
		{
			return this._cbLogSetPreferences(timeFormat, timeZone, units);
		}

		public int cbLogGetPreferences(ref TimeFormat timeFormat, ref TimeZone timeZone, ref TempScale units)
		{
			return this._cbLogGetPreferences(ref timeFormat, ref timeZone, ref units);
		}

		public int cbLogGetFileName(int fileNumber, ref string path, ref string filename)
		{
			return this._cbLogGetFileName(fileNumber, ref path, ref filename);
		}

		public int cbLogGetFileInfo(string filename, ref int version, ref int size)
		{
			return this._cbLogGetFileInfo(filename, ref version, ref size);
		}

		public int cbLogGetSampleInfo(string filename, ref int sampleInterval, ref int sampleCount, ref int startDate, ref int startTime)
		{
			return this._cbLogGetSampleInfo(filename, ref sampleInterval, ref sampleCount, ref startDate, ref startTime);
		}

		public int cbLogGetAIChannelCount(string filename, ref int aiCount)
		{
			return this._cbLogGetAIChannelCount(filename, ref aiCount);
		}

		public int cbLogGetAIInfo(string filename, int[] channelNumbers, int[] units)
		{
			return this._cbLogGetAIInfo(filename, channelNumbers, units);
		}

		public int cbLogGetCJCInfo(string filename, ref int cjcCount)
		{
			return this._cbLogGetCJCInfo(filename, ref cjcCount);
		}

		public int cbLogGetDIOInfo(string filename, ref int dioCount)
		{
			return this._cbLogGetDIOInfo(filename, ref dioCount);
		}

		public int cbLogReadTimeTags(string filename, int startSample, int count, int[] dateTags, int[] timeTags)
		{
			return this._cbLogReadTimeTags(filename, startSample, count, dateTags, timeTags);
		}

		public int cbLogReadAIChannels(string filename, int startSample, int count, float[] aiChannels)
		{
			return this._cbLogReadAIChannels(filename, startSample, count, aiChannels);
		}

		public int cbLogReadCJCChannels(string filename, int startSample, int count, float[] cjcChannels)
		{
			return this._cbLogReadCJCChannels(filename, startSample, count, cjcChannels);
		}

		public int cbLogReadDIOChannels(string filename, int startSample, int count, int[] dioChannels)
		{
			return this._cbLogReadDIOChannels(filename, startSample, count, dioChannels);
		}

		public int cbLogConvertFile(ref string srcFilename, ref string destFileName, int startSample, int count, FieldDelimiter delimiter)
		{
			return this._cbLogConvertFile(ref srcFilename, ref destFileName, startSample, count, delimiter);
		}

		public int cbACalibrateData_noncls_Obsolete(int BoardNum, int NumPoints, int Gain, ref ushort ADData)
		{
			return this._cbACalibrateData_noncls_Obsolete(BoardNum, NumPoints, Gain, ref ADData);
		}

		public int cbAConvertData_noncls_Obsolete(int BoardNum, int NumPoints, ref ushort ADData, out ushort ChanTags)
		{
			return this._cbAConvertData_noncls_Obsolete(BoardNum, NumPoints, ref ADData, out ChanTags);
		}

		public int cbAConvertPretrigData_noncls_Obsolete(int BoardNum, int PreTrigCount, int TotalCount, ref ushort ADData, out ushort ChanTags)
		{
			return this._cbAConvertPretrigData_noncls_Obsolete(BoardNum, PreTrigCount, TotalCount, ref ADData, out ChanTags);
		}

		public int cbAIn_noncls(int BoardNum, int Chan, int Gain, out ushort DataValue)
		{
			return this._cbAIn_noncls(BoardNum, Chan, Gain, out DataValue);
		}

		public int cbAIn32_noncls(int BoardNum, int Chan, int Gain, out uint DataValue, int Options)
		{
			return this._cbAIn32_noncls(BoardNum, Chan, Gain, out DataValue, Options);
		}

		public int cbATrig_noncls(int BoardNum, int Chan, int TrigType, ushort TrigValue, int Gain, out ushort DataValue)
		{
			return this._cbATrig_noncls(BoardNum, Chan, TrigType, TrigValue, Gain, out DataValue);
		}

		public int cbCFreqIn_noncls(int BoardNum, int SigSource, int GateInterval, out ushort Count, out int Freq)
		{
			return this._cbCFreqIn_noncls(BoardNum, SigSource, GateInterval, out Count, out Freq);
		}

		public int cbCIn_noncls(int BoardNum, int CounterNum, out ushort Count)
		{
			return this._cbCIn_noncls(BoardNum, CounterNum, out Count);
		}

		public int cbCIn32_noncls(int BoardNum, int CounterNum, out uint Count)
		{
			return this._cbCIn32_noncls(BoardNum, CounterNum, out Count);
		}

		public int cbCIn64_noncls(int BoardNum, int CounterNum, out ulong Count)
		{
			return this._cbCIn64_noncls(BoardNum, CounterNum, out Count);
		}

		public int cbDIn_noncls(int BoardNum, int PortNum, out ushort DataValue)
		{
			return this._cbDIn_noncls(BoardNum, PortNum, out DataValue);
		}

		public int cbMemWrite_noncls_Obsolete(int BoardNum, ref ushort DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemWrite_noncls_Obsolete(BoardNum, ref DataBuffer, FirstPoint, Count);
		}

		public int cbMemRead_noncls_Obsolete(int BoardNum, out ushort DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemRead_noncls_Obsolete(BoardNum, out DataBuffer, FirstPoint, Count);
		}

		public int cbMemReadPretrig_noncls_Obsolete(int BoardNum, out ushort DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemReadPretrig_noncls_Obsolete(BoardNum, out DataBuffer, FirstPoint, Count);
		}

		public int cbFromEngUnits_noncls(int BoardNum, int Range, float EngUnits, out ushort DataVal)
		{
			return this._cbFromEngUnits_noncls(BoardNum, Range, EngUnits, out DataVal);
		}

		public int cbFileRead_noncls_Obsolete(string fileName, int firstPoint, ref int numPoints, out ushort dataBuffer)
		{
			return this._cbFileRead_noncls_Obsolete(fileName, firstPoint, ref numPoints, out dataBuffer);
		}

		public int cbWinBufToArray_noncls(int memHandle, out ushort dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray_noncls_Obsolete(memHandle, out dataArray, startPoint, count);
		}

		public int cbWinBufToArray32_noncls_Obsolete(int memHandle, out uint dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray32_noncls_Obsolete(memHandle, out dataArray, startPoint, count);
		}

		public int cbWinArrayToBuf_noncls_Obsolete(ref ushort DataArray, int MemHandle, int StartPt, int Count)
		{
			return this._cbWinArrayToBuf_noncls_Obsolete(ref DataArray, MemHandle, StartPt, Count);
		}

		public int cbACalibrateData(int BoardNum, int NumPoints, int Gain, short[] ADData)
		{
			return this._cbACalibrateData(BoardNum, NumPoints, Gain, ADData);
		}

		public int cbAConvertData(int BoardNum, int NumPoints, short[] ADData, short[] ChanTags)
		{
			return this._cbAConvertData(BoardNum, NumPoints, ADData, ChanTags);
		}

		public int cbAConvertPretrigData(int BoardNum, int PreTrigCount, int TotalCount, short[] ADData, short[] ChanTags)
		{
			return this._cbAConvertPretrigData(BoardNum, PreTrigCount, TotalCount, ADData, ChanTags);
		}

		public int cbAInScan(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, IntPtr MemHandle, int Options)
		{
			return this._cbAInScan(BoardNum, LowChan, HighChan, Count, ref Rate, Gain, MemHandle, Options);
		}

		public int cbAOutScan(int BoardNum, int LowChan, int HighChan, int Count, ref int Rate, int Gain, IntPtr MemHandle, int Options)
		{
			return this._cbAOutScan(BoardNum, LowChan, HighChan, Count, ref Rate, Gain, MemHandle, Options);
		}

		public int cbAPretrig(int BoardNum, int LowChan, int HighChan, ref int PreTrigCount, ref int TotalCount, ref int Rate, int Gain, IntPtr MemHandle, int Options)
		{
			return this._cbAPretrig(BoardNum, LowChan, HighChan, ref PreTrigCount, ref TotalCount, ref Rate, Gain, MemHandle, Options);
		}

		public int cbCStoreOnInt(int BoardNum, int IntCount, CounterControl[] CntrControl, IntPtr MemHandle)
		{
			return this._cbCStoreOnInt(BoardNum, IntCount, CntrControl, MemHandle);
		}

		public int cbCInScan(int BoardNum, int FirstCtr, int LastCtr, int Count, ref int Rate, IntPtr MemHandle, uint Options)
		{
			return this._cbCInScan(BoardNum, FirstCtr, LastCtr, Count, ref Rate, MemHandle, Options);
		}

		public int cbDInScan(int BoardNum, int PortNum, int Count, ref int Rate, IntPtr MemHandle, int Options)
		{
			return this._cbDInScan(BoardNum, PortNum, Count, ref Rate, MemHandle, Options);
		}

		public int cbDOutScan(int BoardNum, int PortNum, int Count, ref int Rate, IntPtr MemHandle, int Options)
		{
			return this._cbDOutScan(BoardNum, PortNum, Count, ref Rate, MemHandle, Options);
		}

		public int cbWinBufToEngUnits(int BoardNum, short[] GainArray, int GainCount, IntPtr MemHandle, float[] EngUnits, int FirstPoint, int Count)
		{
			return this._cbWinBufToEngUnits(BoardNum, GainArray, GainCount, MemHandle, EngUnits, FirstPoint, Count);
		}

		public int cbWinBufFromEngUnits(int BoardNum, short[] GainArray, int GainCount, float[] EngUnits, IntPtr MemHandle, int FirstPoint, int Count)
		{
			return this._cbWinBufFromEngUnits(BoardNum, GainArray, GainCount, EngUnits, MemHandle, FirstPoint, Count);
		}

		public int cbDaqInScan(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, ref int PretrigCount, ref int TotalCount, IntPtr MemHandle, int Options)
		{
			return this._cbDaqInScan(BoardNum, ChanArray, ChanTypeArray, GainArray, ChanCount, ref Rate, ref PretrigCount, ref TotalCount, MemHandle, Options);
		}

		public int cbDaqOutScan(int BoardNum, short[] ChanArray, short[] ChanTypeArray, short[] GainArray, int ChanCount, ref int Rate, int Count, IntPtr MemHandle, int Options)
		{
			return this._cbDaqOutScan(BoardNum, ChanArray, ChanTypeArray, GainArray, ChanCount, ref Rate, Count, MemHandle, Options);
		}

		public int cbGetTCValues(int BoardNum, short[] ChanArray, short[] ChanTypeArray, int ChanCount, IntPtr MemHandle, int FirstPoint, int Count, int Scale, float[] TempValArray)
		{
			return this._cbGetTCValues(BoardNum, ChanArray, ChanTypeArray, ChanCount, MemHandle, FirstPoint, Count, Scale, TempValArray);
		}

		public int cbTInScan(int BoardNum, int LowChan, int HighChan, int Scale, float[] DataBuffer, int Options)
		{
			return this._cbTInScan(BoardNum, LowChan, HighChan, Scale, DataBuffer, Options);
		}

		public int cbMemRead(int BoardNum, short[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemRead(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbMemWrite(int BoardNum, short[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemWrite(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbMemReadPretrig(int BoardNum, short[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemReadPretrig(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbFileRead(string fileName, int firstPoint, ref int numPoints, short[] dataBuffer)
		{
			return this._cbFileRead(fileName, firstPoint, ref numPoints, dataBuffer);
		}

		public int cbWinBufToArray(IntPtr MemHandle, short[] dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray(MemHandle, dataArray, startPoint, count);
		}

		public int cbWinBufToArray32(IntPtr MemHandle, int[] dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray32(MemHandle, dataArray, startPoint, count);
		}

		public int cbScaledWinBufToArray(IntPtr MemHandle, double[] dataArray, int startPoint, int count)
		{
			return this._cbScaledWinBufToArray(MemHandle, dataArray, startPoint, count);
		}

		public int cbWinArrayToBuf(short[] DataArray, IntPtr MemHandle, int StartPt, int Count)
		{
			return this._cbWinArrayToBuf(DataArray, MemHandle, StartPt, Count);
		}

		public int cbScaledWinArrayToBuf(double[] DataArray, IntPtr MemHandle, int StartPt, int Count)
		{
			return this._cbScaledWinArrayToBuf(DataArray, MemHandle, StartPt, Count);
		}

		public IntPtr cbWinBufAlloc(int NumPoints)
		{
			return this._cbWinBufAlloc(NumPoints);
		}

		public IntPtr cbWinBufAlloc32(int NumPoints)
		{
			return this._cbWinBufAlloc32(NumPoints);
		}

		public IntPtr cbWinBufAlloc64(int NumPoints)
		{
			return this._cbWinBufAlloc64(NumPoints);
		}

		public IntPtr cbScaledWinBufAlloc(int NumPoints)
		{
			return this._cbScaledWinBufAlloc(NumPoints);
		}

		public int cbWinBufFree(IntPtr MemHandle)
		{
			return this._cbWinBufFree(MemHandle);
		}

		public int cbACalibrateData_noncls(int BoardNum, int NumPoints, int Gain, ushort[] ADData)
		{
			return this._cbACalibrateData_noncls(BoardNum, NumPoints, Gain, ADData);
		}

		public int cbAConvertData_noncls(int BoardNum, int NumPoints, ushort[] ADData, ushort[] ChanTags)
		{
			return this._cbAConvertData_noncls(BoardNum, NumPoints, ADData, ChanTags);
		}

		public int cbAConvertPretrigData_noncls(int BoardNum, int PreTrigCount, int TotalCount, ushort[] ADData, ushort[] ChanTags)
		{
			return this._cbAConvertPretrigData_noncls(BoardNum, PreTrigCount, TotalCount, ADData, ChanTags);
		}

		public int cbMemWrite_noncls(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemWrite_noncls(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbMemRead_noncls(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemRead_noncls(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbMemReadPretrig_noncls(int BoardNum, ushort[] DataBuffer, int FirstPoint, int Count)
		{
			return this._cbMemReadPretrig_noncls(BoardNum, DataBuffer, FirstPoint, Count);
		}

		public int cbFileRead_noncls(string fileName, int firstPoint, ref int numPoints, ushort[] dataBuffer)
		{
			return this._cbFileRead_noncls(fileName, firstPoint, ref numPoints, dataBuffer);
		}

		public int cbWinBufToArray_noncls(IntPtr memHandle, ushort[] dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray_noncls(memHandle, dataArray, startPoint, count);
		}

		public int cbWinBufToArray32_noncls(IntPtr memHandle, uint[] dataArray, int startPoint, int count)
		{
			return this._cbWinBufToArray32_noncls(memHandle, dataArray, startPoint, count);
		}

		public int cbWinArrayToBuf_noncls(ushort[] DataArray, IntPtr MemHandle, int StartPt, int Count)
		{
			return this._cbWinArrayToBuf_noncls(DataArray, MemHandle, StartPt, Count);
		}
	}
}
