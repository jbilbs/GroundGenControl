using System;
using System.ComponentModel;
using System.Text;

namespace MccDaq
{
	public class MccBoard
	{
		public const int Idle = 0;

		public const int Running = 1;

		public const int NotUsed = -1;

		public const int FromHere = -1;

		private int fBoardNum;

		private cBoardConfig fBoardConfig;

		private cDioConfig fDioConfig;

		private cCtrConfig fCtrConfig;

		private cExpansionConfig fExpansionConfig;

		private ErrorInfo m_errorInfo;

		public int BoardNum => this.fBoardNum;

		public string BoardName
		{
			get
			{
				string result = new string(' ', 25);
				CbwApi.Instance.cbGetBoardName(this.fBoardNum, ref result);
				return result;
			}
		}

		public cBoardConfig BoardConfig => this.fBoardConfig;

		public cDioConfig DioConfig => this.fDioConfig;

		public cCtrConfig CtrConfig => this.fCtrConfig;

		public cExpansionConfig ExpansionConfig => this.fExpansionConfig;

		public MccBoard()
			: this(0)
		{
		}

		public MccBoard(int boardNum)
		{
			this.fBoardNum = boardNum;
			this.fBoardConfig = new cBoardConfig(this);
			this.fDioConfig = new cDioConfig(this);
			this.fCtrConfig = new cCtrConfig(this);
			this.fExpansionConfig = new cExpansionConfig(this);
			this.m_errorInfo = new ErrorInfo(0);
		}

		[Obsolete("Use ACalibrateData which has short[] type for the adData parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo ACalibrateData(int numPoints, Range range, ref short adData)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbACalibrateData_Obsolete(this.fBoardNum, numPoints, (int)range, ref adData);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo ACalibrateData(int numPoints, Range range, short[] adData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbACalibrateData(this.fBoardNum, numPoints, (int)range, adData);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AConvertData which has short[] type for the adData parameter")]
		public ErrorInfo AConvertData(int numPoints, ref short adData, out short chanTags)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertData_Obsolete(this.fBoardNum, numPoints, ref adData, out chanTags);
			}
			else
			{
				chanTags = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo AConvertData(int numPoints, short[] adData, short[] chanTags)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertData(this.fBoardNum, numPoints, adData, chanTags);
			return this.m_errorInfo;
		}

		[Obsolete("Use AConvertPretrigData which has short[] type for the adData parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo AConvertPretrigData(int preTrigCount, int totalCount, ref short adData, out short chanTags)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertPretrigData_Obsolete(this.fBoardNum, preTrigCount, totalCount, ref adData, out chanTags);
			}
			else
			{
				chanTags = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo AConvertPretrigData(int preTrigCount, int totalCount, short[] adData, short[] chanTags)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertPretrigData(this.fBoardNum, preTrigCount, totalCount, adData, chanTags);
			return this.m_errorInfo;
		}

		public ErrorInfo AIn(int channel, Range range, out short dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAIn(this.fBoardNum, channel, (int)range, out dataValue);
			return this.m_errorInfo;
		}

		public ErrorInfo AIn32(int channel, Range range, out int dataValue, int options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAIn32(this.fBoardNum, channel, (int)range, out dataValue, options);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AInScan which has IntPtr type for the memHandle parameter")]
		public ErrorInfo AInScan(int lowChan, int highChan, int numPoints, ref int rate, Range range, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAInScan_Obsolete(this.fBoardNum, lowChan, highChan, numPoints, ref rate, (int)range, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo AInScan(int lowChan, int highChan, int numPoints, ref int rate, Range range, IntPtr memHandle, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAInScan(this.fBoardNum, lowChan, highChan, numPoints, ref rate, (int)range, memHandle, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo ALoadQueue(short[] chanArray, Range[] gainArray, int count)
		{
			short[] array = new short[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (short)gainArray[i];
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbALoadQueue(this.fBoardNum, chanArray, array, count);
			return this.m_errorInfo;
		}

		public ErrorInfo AOut(int channel, Range range, short dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAOut(this.fBoardNum, channel, (int)range, (ushort)dataValue);
			return this.m_errorInfo;
		}

		[Obsolete("Use AOutScan which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo AOutScan(int lowChan, int highChan, int numPoints, ref int rate, Range range, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAOutScan_Obsolete(this.fBoardNum, lowChan, highChan, numPoints, ref rate, (int)range, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo AOutScan(int lowChan, int highChan, int numPoints, ref int rate, Range range, IntPtr memHandle, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAOutScan(this.fBoardNum, lowChan, highChan, numPoints, ref rate, (int)range, memHandle, (int)options);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use APretrig which has IntPtr type for the memHandle parameter")]
		public ErrorInfo APretrig(int lowChan, int highChan, ref int pretrigCount, ref int totalCount, ref int rate, Range range, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAPretrig_Obsolete(this.fBoardNum, lowChan, highChan, ref pretrigCount, ref totalCount, ref rate, (int)range, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo APretrig(int lowChan, int highChan, ref int pretrigCount, ref int totalCount, ref int rate, Range range, IntPtr memHandle, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAPretrig(this.fBoardNum, lowChan, highChan, ref pretrigCount, ref totalCount, ref rate, (int)range, memHandle, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo ATrig(int chan, TriggerType trigType, short trigValue, Range range, out short dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbATrig(this.fBoardNum, chan, (int)trigType, trigValue, (int)range, out dataValue);
			return this.m_errorInfo;
		}

		public ErrorInfo C7266Config(int counterNum, Quadrature quadrature, CountingMode countingMode, DataEncoding dataEncoding, IndexMode indexMode, OptionState invertIndex, FlagPins flagPins, OptionState gateState)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC7266Config(this.fBoardNum, counterNum, (int)quadrature, (int)countingMode, (int)dataEncoding, (int)indexMode, (int)invertIndex, (int)flagPins, (int)gateState);
			return this.m_errorInfo;
		}

		public ErrorInfo C8254Config(int counterNum, C8254Mode config)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC8254Config(this.fBoardNum, counterNum, (int)config);
			return this.m_errorInfo;
		}

		public ErrorInfo C8536Config(int counterNum, C8536OutputControl outputControl, RecycleMode recycleMode, OptionState retrigger)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC8536Config(this.fBoardNum, counterNum, (int)outputControl, (int)recycleMode, (int)retrigger);
			return this.m_errorInfo;
		}

		public ErrorInfo C8536Config(int counterNum, C8536OutputControl outputControl, RecycleMode recycleMode, C8536TriggerType trigType)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC8536Config(this.fBoardNum, counterNum, (int)outputControl, (int)recycleMode, (int)trigType);
			return this.m_errorInfo;
		}

		public ErrorInfo C9513Config(int counterNum, GateControl gateControl, CountEdge counterEdge, CounterSource counterSource, OptionState specialGate, Reload reload, RecycleMode recycleMode, BCDMode bcdMode, CountDirection countDirection, C9513OutputControl outputControl)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC9513Config(this.fBoardNum, counterNum, (int)gateControl, (int)counterEdge, (int)counterSource, (int)specialGate, (int)reload, (int)recycleMode, (int)bcdMode, (int)countDirection, (int)outputControl);
			return this.m_errorInfo;
		}

		public ErrorInfo C8536Init(int chipNum, CtrlOutput ctr1Output)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC8536Init(this.fBoardNum, chipNum, (int)ctr1Output);
			return this.m_errorInfo;
		}

		public ErrorInfo C9513Init(int chipNum, int foutDivider, CounterSource foutSource, CompareValue compare1, CompareValue compare2, TimeOfDay timeOfDay)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbC9513Init(this.fBoardNum, chipNum, foutDivider, (int)foutSource, (int)compare1, (int)compare2, (int)timeOfDay);
			return this.m_errorInfo;
		}

		public ErrorInfo CFreqIn(SignalSource signalSource, int gateInterval, out short count, out int freq)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCFreqIn(this.fBoardNum, (int)signalSource, gateInterval, out count, out freq);
			return this.m_errorInfo;
		}

		public ErrorInfo CIn(int counterNum, out short count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		public ErrorInfo CIn32(int counterNum, out int count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn32(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		public ErrorInfo CIn64(int counterNum, out long count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn64(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		public ErrorInfo CLoad(CounterRegister regNum, int loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad(this.fBoardNum, (int)regNum, (uint)loadValue);
			return this.m_errorInfo;
		}

		public ErrorInfo CLoad32(CounterRegister regNum, int loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad32(this.fBoardNum, (int)regNum, (uint)loadValue);
			return this.m_errorInfo;
		}

		public ErrorInfo CLoad64(CounterRegister regNum, long loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad64(this.fBoardNum, (int)regNum, (ulong)loadValue);
			return this.m_errorInfo;
		}

		public ErrorInfo CStatus(int counterNum, out StatusBits statusBits)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCStatus(this.fBoardNum, counterNum, out uint num);
			statusBits = (StatusBits)num;
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use CStoreOnInt which has IntPtr type for the memHandle parameter")]
		public ErrorInfo CStoreOnInt(int intCount, ref CounterControl cntrControl, int memHandle)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCStoreOnInt_Obsolete(this.fBoardNum, intCount, ref cntrControl, memHandle);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo CStoreOnInt(int intCount, CounterControl[] cntrControl, IntPtr memHandle)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCStoreOnInt(this.fBoardNum, intCount, cntrControl, memHandle);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use CInScan which has IntPtr type for the memHandle parameter")]
		public ErrorInfo CInScan(int firstCtr, int lastCtr, int numPoints, ref int rate, int memHandle, ScanOptions Options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCInScan_Obsolete(this.fBoardNum, firstCtr, lastCtr, numPoints, ref rate, memHandle, (uint)Options);
			return this.m_errorInfo;
		}

		public ErrorInfo CInScan(int firstCtr, int lastCtr, int numPoints, ref int rate, IntPtr memHandle, ScanOptions Options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCInScan(this.fBoardNum, firstCtr, lastCtr, numPoints, ref rate, memHandle, (uint)Options);
			return this.m_errorInfo;
		}

		public ErrorInfo CConfigScan(int counterNum, CounterMode mode, CounterDebounceTime debounceTime, CounterDebounceMode debounceMode, CounterEdgeDetection edgeDetection, CounterTickSize tickSize, int mapCounter)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCConfigScan_Obsolete(this.fBoardNum, counterNum, (uint)mode, (uint)debounceTime, (uint)debounceMode, (uint)edgeDetection, (uint)tickSize, (uint)mapCounter);
			return this.m_errorInfo;
		}

		[Obsolete("Use CConfigScan which has CounterTickSize for the tickSize parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo CConfigScan(int counterNum, CounterMode mode, CounterDebounceTime debounceTime, CounterDebounceMode debounceMode, CounterEdgeDetection edgeDetection, int tickSize, int mapCounter)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCConfigScan_Obsolete(this.fBoardNum, counterNum, (uint)mode, (uint)debounceTime, (uint)debounceMode, (uint)edgeDetection, (uint)tickSize, (uint)mapCounter);
			return this.m_errorInfo;
		}

		public ErrorInfo CClear(int counterNum)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCClear(this.fBoardNum, counterNum);
			return this.m_errorInfo;
		}

		public ErrorInfo TimerOutStart(int timerNum, ref double frequency)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTimerOutStart(this.fBoardNum, timerNum, ref frequency);
			return this.m_errorInfo;
		}

		public ErrorInfo TimerOutStop(int timerNum)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTimerOutStop(this.fBoardNum, timerNum);
			return this.m_errorInfo;
		}

		public ErrorInfo PulseOutStart(int timerNum, ref double frequency, ref double dutyCycle, int pulseCount, ref double initialDelay, IdleState idleState, PulseOutOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbPulseOutStart(this.fBoardNum, timerNum, ref frequency, ref dutyCycle, (uint)pulseCount, ref initialDelay, (int)idleState, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo PulseOutStop(int timerNum)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbPulseOutStop(this.fBoardNum, timerNum);
			return this.m_errorInfo;
		}

		public ErrorInfo DBitIn(DigitalPortType portType, int bitNum, out DigitalLogicState bitValue)
		{
			ushort num = 0;
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDBitIn(this.fBoardNum, (int)portType, bitNum, out num);
			bitValue = (DigitalLogicState)num;
			return this.m_errorInfo;
		}

		public ErrorInfo DBitOut(DigitalPortType portType, int bitNum, DigitalLogicState bitValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDBitOut(this.fBoardNum, (int)portType, bitNum, (ushort)bitValue);
			return this.m_errorInfo;
		}

		public ErrorInfo DConfigPort(DigitalPortType portNum, DigitalPortDirection direction)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDConfigPort(this.fBoardNum, (int)portNum, (int)direction);
			return this.m_errorInfo;
		}

		public ErrorInfo DConfigBit(DigitalPortType portNum, int bitNum, DigitalPortDirection direction)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDConfigBit(this.fBoardNum, (int)portNum, bitNum, (int)direction);
			return this.m_errorInfo;
		}

		public ErrorInfo DIn(DigitalPortType portNum, out short dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDIn(this.fBoardNum, (int)portNum, out dataValue);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use DInScan which has IntPtr type for the memHandle parameter")]
		public ErrorInfo DInScan(DigitalPortType portNum, int numPoints, ref int rate, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDInScan_Obsolete(this.fBoardNum, (int)portNum, numPoints, ref rate, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo DInScan(DigitalPortType portNum, int numPoints, ref int rate, IntPtr memHandle, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDInScan(this.fBoardNum, (int)portNum, numPoints, ref rate, memHandle, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo DOut(DigitalPortType portNum, short dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDOut(this.fBoardNum, (int)portNum, (ushort)dataValue);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use DOutScan which has IntPtr type for the memHandle parameter")]
		public ErrorInfo DOutScan(DigitalPortType portNum, int count, ref int rate, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDOutScan_Obsolete(this.fBoardNum, (int)portNum, count, ref rate, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo DOutScan(DigitalPortType portNum, int count, ref int rate, IntPtr memHandle, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDOutScan(this.fBoardNum, (int)portNum, count, ref rate, memHandle, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo FileAInScan(int lowChan, int highChan, int numPoints, ref int rate, Range range, string fileName, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileAInScan(this.fBoardNum, lowChan, highChan, numPoints, ref rate, (int)range, fileName, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo FilePretrig(int lowChan, int highChan, ref int pretrigCount, ref int totalCount, ref int rate, Range range, string fileName, ScanOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbFilePretrig(this.fBoardNum, lowChan, highChan, ref pretrigCount, ref totalCount, ref rate, (int)range, fileName, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo FlashLED()
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbFlashLED(this.fBoardNum);
			return this.m_errorInfo;
		}

		public ErrorInfo HideLoginDialog(bool hide)
		{
			this.m_errorInfo = this.SetConfig(InfoType.BoardInfo, 0, 274, hide ? 1 : 0);
			return this.m_errorInfo;
		}

		public ErrorInfo DeviceLogin(string userName, string password)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDeviceLogin(this.fBoardNum, ref userName, ref password);
			return this.m_errorInfo;
		}

		public ErrorInfo DeviceLogout()
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDeviceLogout(this.fBoardNum);
			return this.m_errorInfo;
		}

		public ErrorInfo GetStatus(out short status, out int curCount, out int curIndex, FunctionType functionType)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetIOStatus(this.fBoardNum, out status, out curCount, out curIndex, (int)functionType);
			return this.m_errorInfo;
		}

		public ErrorInfo RS485(OptionState transmit, OptionState receive)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbRS485(this.fBoardNum, (int)transmit, (int)receive);
			return this.m_errorInfo;
		}

		public ErrorInfo StopBackground(FunctionType funcType)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbStopIOBackground(this.fBoardNum, (int)funcType);
			return this.m_errorInfo;
		}

		public ErrorInfo TIn(int chan, TempScale scale, out float tempValue, ThermocoupleOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTIn(this.fBoardNum, chan, (int)scale, out tempValue, (int)options);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use TInScan which has float[] type for the dataBuffer parameter")]
		public ErrorInfo TInScan(int lowChan, int highChan, TempScale scale, out float dataBuffer, ThermocoupleOptions options)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTInScan_Obsolete(this.fBoardNum, lowChan, highChan, (int)scale, out dataBuffer, (int)options);
			}
			else
			{
				dataBuffer = 0f;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo TInScan(int lowChan, int highChan, TempScale scale, float[] dataBuffer, ThermocoupleOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTInScan(this.fBoardNum, lowChan, highChan, (int)scale, dataBuffer, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo MemSetDTMode(DTMode mode)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemSetDTMode(this.fBoardNum, (int)mode);
			return this.m_errorInfo;
		}

		public ErrorInfo MemReset()
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemReset(this.fBoardNum);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MemRead which has short[] type for the dataBuffer parameter")]
		public ErrorInfo MemRead(out short dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemRead_Obsolete(this.fBoardNum, out dataBuffer, firstPoint, numPoints);
			}
			else
			{
				dataBuffer = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo MemRead(short[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemRead(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		[Obsolete("Use MemWrite which has short[] type for the dataBuffer parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo MemWrite(ref short dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemWrite_Obsolete(this.fBoardNum, ref dataBuffer, firstPoint, numPoints);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo MemWrite(short[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemWrite(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MemReadPretrig which has short[] type for the dataBuffer parameter")]
		public ErrorInfo MemReadPretrig(out short dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemReadPretrig_Obsolete(this.fBoardNum, out dataBuffer, firstPoint, numPoints);
			}
			else
			{
				dataBuffer = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo MemReadPretrig(short[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemReadPretrig(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		public int InByte(int portNum)
		{
			return CbwApi.Instance.cbInByte(this.fBoardNum, portNum);
		}

		public ErrorInfo OutByte(int portNum, int portVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbOutByte(this.fBoardNum, portNum, portVal);
			return this.m_errorInfo;
		}

		public int InWord(int portNum)
		{
			return CbwApi.Instance.cbInWord(this.fBoardNum, portNum);
		}

		public ErrorInfo OutWord(int portNum, int portVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbOutWord(this.fBoardNum, portNum, portVal);
			return this.m_errorInfo;
		}

		internal ErrorInfo GetConfig(InfoType infoType, int devNum, int configItem, out string configVal, ref int maxCount)
		{
			StringBuilder stringBuilder = new StringBuilder(maxCount + 1);
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetConfigString((int)infoType, this.fBoardNum, devNum, configItem, stringBuilder, ref maxCount);
			if (maxCount > 0)
			{
				configVal = stringBuilder.ToString().Substring(0, maxCount);
			}
			else
			{
				configVal = "";
			}
			return this.m_errorInfo;
		}

		internal ErrorInfo GetConfig(InfoType infoType, int devNum, int configItem, out int configVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetConfig((int)infoType, this.fBoardNum, devNum, configItem, out configVal);
			return this.m_errorInfo;
		}

		internal ErrorInfo SetConfig(InfoType infoType, int devNum, int configItem, int configVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetConfig((int)infoType, this.fBoardNum, devNum, configItem, configVal);
			return this.m_errorInfo;
		}

		internal ErrorInfo SetConfig(InfoType infoType, int devNum, int configItem, string configVal, ref int len)
		{
			if (len > configVal.Length)
			{
				len = configVal.Length;
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetConfigString((int)infoType, this.fBoardNum, devNum, configItem, ref configVal, ref len);
			return this.m_errorInfo;
		}

		public ErrorInfo GetConfig(int infoType, int devNum, int configItem, out int configVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetConfig(infoType, this.fBoardNum, devNum, configItem, out configVal);
			return this.m_errorInfo;
		}

		public ErrorInfo SetConfig(int infoType, int devNum, int configItem, int configVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetConfig(infoType, this.fBoardNum, devNum, configItem, configVal);
			return this.m_errorInfo;
		}

		public ErrorInfo GetConfigString(int infoType, int devNum, int configItem, out string configVal, ref int maxCount)
		{
			StringBuilder stringBuilder = new StringBuilder(maxCount + 1);
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetConfigString(infoType, this.fBoardNum, devNum, configItem, stringBuilder, ref maxCount);
			if (maxCount > 0)
			{
				configVal = stringBuilder.ToString().Substring(0, maxCount);
			}
			else
			{
				configVal = "";
			}
			return this.m_errorInfo;
		}

		public ErrorInfo SetConfigString(int infoType, int devNum, int configItem, string configVal, ref int len)
		{
			if (len > configVal.Length)
			{
				len = configVal.Length;
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetConfigString(infoType, this.fBoardNum, devNum, configItem, ref configVal, ref len);
			return this.m_errorInfo;
		}

		public ErrorInfo ToEngUnits(Range range, short dataVal, out float engUnits)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbToEngUnits(this.fBoardNum, (int)range, (ushort)dataVal, out engUnits);
			return this.m_errorInfo;
		}

		public ErrorInfo ToEngUnits32(Range range, int dataVal, out double engUnits)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbToEngUnits32(this.fBoardNum, (int)range, (uint)dataVal, out engUnits);
			return this.m_errorInfo;
		}

		public ErrorInfo FromEngUnits(Range range, float engUnits, out short dataVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbFromEngUnits(this.fBoardNum, (int)range, engUnits, out dataVal);
			return this.m_errorInfo;
		}

		public ErrorInfo SetTrigger(TriggerType trigType, short lowThreshold, short highThreshold)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetTrigger(this.fBoardNum, (int)trigType, (ushort)lowThreshold, (ushort)highThreshold);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo EnableEvent(EventType eventType, int eventParameter, EventCallback callbackFunc, IntPtr userData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbEnableEvent_noncls(this.fBoardNum, (uint)eventType, (uint)eventParameter, callbackFunc, userData);
			return this.m_errorInfo;
		}

		public ErrorInfo EnableEvent(EventType eventType, int eventParameter, CallbackFunction callbackFunc, IntPtr userData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbEnableEvent(this.fBoardNum, (uint)eventType, (uint)eventParameter, callbackFunc, userData);
			return this.m_errorInfo;
		}

		public ErrorInfo EnableEvent(EventType eventType, EventParameter eventParameter, CallbackFunction callbackFunc, IntPtr userData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbEnableEvent(this.fBoardNum, (uint)eventType, (uint)eventParameter, callbackFunc, userData);
			return this.m_errorInfo;
		}

		public ErrorInfo DisableEvent(EventType eventType)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDisableEvent(this.fBoardNum, (uint)eventType);
			return this.m_errorInfo;
		}

		public ErrorInfo SelectSignal(SignalDirection direction, SignalType signalType, ConnectionPin connectionPin, SignalPolarity polarity)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSelectSignal(this.fBoardNum, (int)direction, (int)signalType, (int)connectionPin, (int)polarity);
			return this.m_errorInfo;
		}

		public ErrorInfo GetSignal(SignalDirection direction, SignalType signalType, int index, out ConnectionPin connectionPin, out SignalPolarity signalPolarity)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetSignal(this.fBoardNum, (int)direction, (int)signalType, index, out int num, out int num2);
			signalPolarity = (SignalPolarity)num2;
			connectionPin = (ConnectionPin)num;
			return this.m_errorInfo;
		}

		[Obsolete("Use WinBufToEngArray which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo WinBufToEngArray(Range Gain, int MemHandle, double[,] EngUnits, int FirstPoint, int Count, int NumChannels)
		{
			if (IntPtr.Size == 4)
			{
				int length = EngUnits.GetLength(0);
				if (length < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
					return this.m_errorInfo;
				}
				float[] array = new float[NumChannels * Count];
				short[] gainArray = new short[1]
				{
					(short)Gain
				};
				FirstPoint *= NumChannels;
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToEngUnits_Obsolete(this.fBoardNum, gainArray, 1, MemHandle, out array[0], FirstPoint, Count * NumChannels);
				if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
				{
					for (int i = 0; i < NumChannels; i++)
					{
						for (int j = 0; j < Count; j++)
						{
							int num = i;
							int num2 = j;
							double num3 = (double)array[i + j * NumChannels];
							EngUnits[num, num2] = num3;
						}
					}
				}
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo WinBufToEngArray(Range Gain, IntPtr MemHandle, double[,] EngUnits, int FirstPoint, int Count, int NumChannels)
		{
			int length = EngUnits.GetLength(0);
			if (length < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
				return this.m_errorInfo;
			}
			float[] array = new float[NumChannels * Count];
			short[] gainArray = new short[1]
			{
				(short)Gain
			};
			FirstPoint *= NumChannels;
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToEngUnits(this.fBoardNum, gainArray, 1, MemHandle, array, FirstPoint, Count * NumChannels);
			if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				for (int i = 0; i < NumChannels; i++)
				{
					for (int j = 0; j < Count; j++)
					{
						int num = i;
						int num2 = j;
						double num3 = (double)array[i + j * NumChannels];
						EngUnits[num, num2] = num3;
					}
				}
			}
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufToEngArray which has IntPtr type for the memHandle parameter")]
		public ErrorInfo WinBufToEngArray(Range[] GainArray, int GainCount, int MemHandle, double[,] EngUnits, int FirstPoint, int Count, int NumChannels)
		{
			if (IntPtr.Size == 4)
			{
				int length = EngUnits.GetLength(0);
				if (length < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
					return this.m_errorInfo;
				}
				int num = GainArray.Length;
				if (num < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidGainArrayLength);
					return this.m_errorInfo;
				}
				float[] array = new float[NumChannels * Count];
				short[] array2 = new short[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = (short)GainArray[i];
				}
				FirstPoint *= NumChannels;
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToEngUnits_Obsolete(this.fBoardNum, array2, GainCount, MemHandle, out array[0], FirstPoint, Count * NumChannels);
				if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
				{
					for (int j = 0; j < NumChannels; j++)
					{
						for (int k = 0; k < Count; k++)
						{
							int num2 = j;
							int num3 = k;
							double num4 = (double)array[j + k * NumChannels];
							EngUnits[num2, num3] = num4;
						}
					}
				}
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo WinBufToEngArray(Range[] GainArray, int GainCount, IntPtr MemHandle, double[,] EngUnits, int FirstPoint, int Count, int NumChannels)
		{
			int length = EngUnits.GetLength(0);
			if (length < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
				return this.m_errorInfo;
			}
			int num = GainArray.Length;
			if (num < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidGainArrayLength);
				return this.m_errorInfo;
			}
			float[] array = new float[NumChannels * Count];
			short[] array2 = new short[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (short)GainArray[i];
			}
			FirstPoint *= NumChannels;
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToEngUnits(this.fBoardNum, array2, GainCount, MemHandle, array, FirstPoint, Count * NumChannels);
			if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				for (int j = 0; j < NumChannels; j++)
				{
					for (int k = 0; k < Count; k++)
					{
						int num2 = j;
						int num3 = k;
						double num4 = (double)array[j + k * NumChannels];
						EngUnits[num2, num3] = num4;
					}
				}
			}
			return this.m_errorInfo;
		}

		[Obsolete("Use EngArrayToWinBuf which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo EngArrayToWinBuf(Range Gain, double[,] EngUnits, int MemHandle, int FirstPoint, int Count, int NumChannels)
		{
			if (IntPtr.Size == 4)
			{
				int length = EngUnits.GetLength(0);
				if (length < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
					return this.m_errorInfo;
				}
				float[] array = new float[NumChannels * Count];
				short[] gainArray = new short[1]
				{
					(short)Gain
				};
				for (int i = 0; i < NumChannels; i++)
				{
					for (int j = 0; j < Count; j++)
					{
						array[i + j * NumChannels] = Convert.ToSingle(EngUnits[i, j]);
					}
				}
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFromEngUnits_Obsolete(this.fBoardNum, gainArray, 1, ref array[0], MemHandle, FirstPoint, Count * NumChannels);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo EngArrayToWinBuf(Range Gain, double[,] EngUnits, IntPtr MemHandle, int FirstPoint, int Count, int NumChannels)
		{
			int length = EngUnits.GetLength(0);
			if (length < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
				return this.m_errorInfo;
			}
			float[] array = new float[NumChannels * Count];
			short[] gainArray = new short[1]
			{
				(short)Gain
			};
			for (int i = 0; i < NumChannels; i++)
			{
				for (int j = 0; j < Count; j++)
				{
					array[i + j * NumChannels] = Convert.ToSingle(EngUnits[i, j]);
				}
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFromEngUnits(this.fBoardNum, gainArray, 1, array, MemHandle, FirstPoint, Count * NumChannels);
			return this.m_errorInfo;
		}

		[Obsolete("Use EngArrayToWinBuf which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo EngArrayToWinBuf(Range[] GainArray, int GainCount, double[,] EngUnits, int MemHandle, int FirstPoint, int Count, int NumChannels)
		{
			if (IntPtr.Size == 4)
			{
				int length = EngUnits.GetLength(0);
				if (length < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
					return this.m_errorInfo;
				}
				int num = GainArray.Length;
				if (num < NumChannels)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidGainArrayLength);
					return this.m_errorInfo;
				}
				float[] array = new float[NumChannels * Count];
				short[] array2 = new short[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = (short)GainArray[i];
				}
				for (int j = 0; j < NumChannels; j++)
				{
					for (int k = 0; k < Count; k++)
					{
						array[j + k * NumChannels] = Convert.ToSingle(EngUnits[j, k]);
					}
				}
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFromEngUnits_Obsolete(this.fBoardNum, array2, 1, ref array[0], MemHandle, FirstPoint, Count * NumChannels);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo EngArrayToWinBuf(Range[] GainArray, int GainCount, double[,] EngUnits, IntPtr MemHandle, int FirstPoint, int Count, int NumChannels)
		{
			int length = EngUnits.GetLength(0);
			if (length < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
				return this.m_errorInfo;
			}
			int num = GainArray.Length;
			if (num < NumChannels)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidGainArrayLength);
				return this.m_errorInfo;
			}
			float[] array = new float[NumChannels * Count];
			short[] array2 = new short[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (short)GainArray[i];
			}
			for (int j = 0; j < NumChannels; j++)
			{
				for (int k = 0; k < Count; k++)
				{
					array[j + k * NumChannels] = Convert.ToSingle(EngUnits[j, k]);
				}
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFromEngUnits(this.fBoardNum, array2, 1, array, MemHandle, FirstPoint, Count * NumChannels);
			return this.m_errorInfo;
		}

		[Obsolete("Use DaqInScan which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo DaqInScan(short[] chanArray, ChannelType[] chanTypeArray, Range[] gainArray, int chanCount, ref int rate, ref int pretrigCount, ref int totalCount, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				short[] array = new short[chanCount];
				short[] array2 = new short[chanCount];
				for (int i = 0; i < chanCount; i++)
				{
					array[i] = (short)chanTypeArray[i];
					array2[i] = (short)gainArray[i];
				}
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqInScan_Obsolete(this.fBoardNum, chanArray, array, array2, chanCount, ref rate, ref pretrigCount, ref totalCount, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo DaqInScan(short[] chanArray, ChannelType[] chanTypeArray, Range[] gainArray, int chanCount, ref int rate, ref int pretrigCount, ref int totalCount, IntPtr memHandle, ScanOptions options)
		{
			short[] array = new short[chanCount];
			short[] array2 = new short[chanCount];
			for (int i = 0; i < chanCount; i++)
			{
				array[i] = (short)chanTypeArray[i];
				array2[i] = (short)gainArray[i];
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqInScan(this.fBoardNum, chanArray, array, array2, chanCount, ref rate, ref pretrigCount, ref totalCount, memHandle, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo DaqSetTrigger(TriggerSource trigSource, TriggerSensitivity trigSense, int trigChan, ChannelType chanType, Range gain, float level, float variance, TriggerEvent trigEvent)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqSetTrigger(this.fBoardNum, (int)trigSource, (int)trigSense, trigChan, (int)chanType, (int)gain, level, variance, (int)trigEvent);
			return this.m_errorInfo;
		}

		public ErrorInfo DaqSetSetpoints(float[] limitAArray, float[] limitBArray, float[] reserved, SetpointFlag[] setpointFlagsArray, SetpointOutput[] setpointOutputArray, float[] output1Array, float[] output2Array, float[] outputMask1Array, float[] outputMask2Array, int setpointCount)
		{
			int[] array = new int[setpointCount];
			int[] array2 = new int[setpointCount];
			for (int i = 0; i < setpointCount; i++)
			{
				array[i] = (int)setpointFlagsArray[i];
				array2[i] = (int)setpointOutputArray[i];
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqSetSetpoints(this.fBoardNum, limitAArray, limitBArray, reserved, array, array2, output1Array, output2Array, outputMask1Array, outputMask1Array, setpointCount);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use DaqOutScan which has IntPtr type for the memHandle parameter")]
		public ErrorInfo DaqOutScan(short[] chanArray, ChannelType[] chanTypeArray, Range[] gainArray, int chanCount, ref int rate, int count, int memHandle, ScanOptions options)
		{
			if (IntPtr.Size == 4)
			{
				short[] array = new short[chanCount];
				short[] array2 = new short[chanCount];
				for (int i = 0; i < chanCount; i++)
				{
					array[i] = (short)chanTypeArray[i];
					array2[i] = (short)gainArray[i];
				}
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqOutScan_Obsolete(this.fBoardNum, chanArray, array, array2, chanCount, ref rate, count, memHandle, (int)options);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo DaqOutScan(short[] chanArray, ChannelType[] chanTypeArray, Range[] gainArray, int chanCount, ref int rate, int count, IntPtr memHandle, ScanOptions options)
		{
			short[] array = new short[chanCount];
			short[] array2 = new short[chanCount];
			for (int i = 0; i < chanCount; i++)
			{
				array[i] = (short)chanTypeArray[i];
				array2[i] = (short)gainArray[i];
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDaqOutScan(this.fBoardNum, chanArray, array, array2, chanCount, ref rate, count, memHandle, (int)options);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetTCValues which has IntPtr type for the memHandle parameter")]
		public ErrorInfo GetTCValues(short[] chanArray, ChannelType[] chanTypeArray, int chanCount, int memHandle, int firstPoint, int count, TempScale scale, out float tempValArray)
		{
			if (IntPtr.Size == 4)
			{
				short[] array = new short[chanCount];
				for (int i = 0; i < chanCount; i++)
				{
					array[i] = (short)chanTypeArray[i];
				}
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetTCValues_Obsolete(this.fBoardNum, chanArray, array, chanCount, memHandle, firstPoint, count, (int)scale, out tempValArray);
			}
			else
			{
				tempValArray = 0f;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo GetTCValues(short[] chanArray, ChannelType[] chanTypeArray, int chanCount, IntPtr memHandle, int firstPoint, int count, TempScale scale, float[] tempValArray)
		{
			short[] array = new short[chanCount];
			for (int i = 0; i < chanCount; i++)
			{
				array[i] = (short)chanTypeArray[i];
			}
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetTCValues(this.fBoardNum, chanArray, array, chanCount, memHandle, firstPoint, count, (int)scale, tempValArray);
			return this.m_errorInfo;
		}

		[Obsolete("Use GetTCValues which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo GetTCValues(short[] chanArray, ChannelType[] chanTypeArray, int chanCount, int memHandle, int firstPoint, int count, TempScale scale, double[,] tempValArray)
		{
			if (IntPtr.Size == 4)
			{
				int num = 0;
				short[] array = new short[chanCount];
				for (int i = 0; i < chanCount; i++)
				{
					array[i] = (short)chanTypeArray[i];
				}
				for (int j = 0; j < chanCount - 1; j++)
				{
					if (chanTypeArray[j] == ChannelType.CJC && chanTypeArray[j + 1] == ChannelType.TC)
					{
						j++;
						num++;
					}
				}
				int length = tempValArray.GetLength(0);
				if (length < num)
				{
					this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
					return this.m_errorInfo;
				}
				float[] array2 = new float[num * count];
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetTCValues_Obsolete(this.fBoardNum, chanArray, array, chanCount, memHandle, firstPoint, count, (int)scale, out array2[0]);
				if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
				{
					for (int k = 0; k < num; k++)
					{
						for (int l = 0; l < count; l++)
						{
							int num2 = k;
							int num3 = l;
							double num4 = (double)array2[k + l * num];
							tempValArray[num2, num3] = num4;
						}
					}
				}
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		public ErrorInfo GetTCValues(short[] chanArray, ChannelType[] chanTypeArray, int chanCount, IntPtr memHandle, int firstPoint, int count, TempScale scale, double[,] tempValArray)
		{
			int num = 0;
			short[] array = new short[chanCount];
			for (int i = 0; i < chanCount; i++)
			{
				array[i] = (short)chanTypeArray[i];
			}
			for (int j = 0; j < chanCount - 1; j++)
			{
				if (chanTypeArray[j] == ChannelType.CJC && chanTypeArray[j + 1] == ChannelType.TC)
				{
					j++;
					num++;
				}
			}
			int length = tempValArray.GetLength(0);
			if (length < num)
			{
				this.m_errorInfo.SetError(ErrorInfo.ErrorCode.InvalidDimension0Length);
				return this.m_errorInfo;
			}
			float[] array2 = new float[num * count];
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetTCValues(this.fBoardNum, chanArray, array, chanCount, memHandle, firstPoint, count, (int)scale, array2);
			if (this.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				for (int k = 0; k < num; k++)
				{
					for (int l = 0; l < count; l++)
					{
						int num2 = k;
						int num3 = l;
						double num4 = (double)array2[k + l * num];
						tempValArray[num2, num3] = num4;
					}
				}
			}
			return this.m_errorInfo;
		}

		public ErrorInfo VIn(int channel, Range range, out float dataValue, VInOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbVIn(this.fBoardNum, channel, (int)range, out dataValue, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo VIn32(int channel, Range range, out double dataValue, VInOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbVIn32(this.fBoardNum, channel, (int)range, out dataValue, (int)options);
			return this.m_errorInfo;
		}

		public ErrorInfo VOut(int channel, Range range, float dataValue, VOutOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbVOut(this.fBoardNum, channel, (int)range, dataValue, (int)options);
			return this.m_errorInfo;
		}

		[Obsolete("Use ACalibrateData which has ushort[] type for the adData parameter")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo ACalibrateData(int numPoints, Range range, ref ushort adData)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbACalibrateData_noncls_Obsolete(this.fBoardNum, numPoints, (int)range, ref adData);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo ACalibrateData(int numPoints, Range range, ushort[] adData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbACalibrateData_noncls(this.fBoardNum, numPoints, (int)range, adData);
			return this.m_errorInfo;
		}

		[Obsolete("Use AConvertData which has ushort[] type for the adData parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public ErrorInfo AConvertData(int numPoints, ref ushort adData, out ushort chanTags)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertData_noncls_Obsolete(this.fBoardNum, numPoints, ref adData, out chanTags);
			}
			else
			{
				chanTags = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo AConvertData(int numPoints, ushort[] adData, ushort[] chanTags)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertData_noncls(this.fBoardNum, numPoints, adData, chanTags);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AConvertPretrigData which has ushort[] type for the adData parameter")]
		[CLSCompliant(false)]
		public ErrorInfo AConvertPretrigData(int preTrigCount, int totalCount, ref ushort adData, out ushort chanTags)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertPretrigData_noncls_Obsolete(this.fBoardNum, preTrigCount, totalCount, ref adData, out chanTags);
			}
			else
			{
				chanTags = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo AConvertPretrigData(int preTrigCount, int totalCount, ushort[] adData, ushort[] chanTags)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAConvertPretrigData_noncls(this.fBoardNum, preTrigCount, totalCount, adData, chanTags);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo AIn(int channel, Range range, out ushort dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAIn_noncls(this.fBoardNum, channel, (int)range, out dataValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo AIn32(int channel, Range range, out uint dataValue, int options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAIn32_noncls(this.fBoardNum, channel, (int)range, out dataValue, options);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo AOut(int channel, Range range, ushort dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbAOut(this.fBoardNum, channel, (int)range, dataValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo ATrig(int chan, TriggerType trigType, ushort trigValue, Range range, out ushort dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbATrig_noncls(this.fBoardNum, chan, (int)trigType, trigValue, (int)range, out dataValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CFreqIn(SignalSource signalSource, int gateInterval, out ushort count, out int freq)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCFreqIn_noncls(this.fBoardNum, (int)signalSource, gateInterval, out count, out freq);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CIn(int counterNum, out ushort count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn_noncls(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CIn32(int counterNum, out uint count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn32_noncls(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CIn64(int counterNum, out ulong count)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCIn64_noncls(this.fBoardNum, counterNum, out count);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CLoad(CounterRegister regNum, uint loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad(this.fBoardNum, (int)regNum, loadValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CLoad32(CounterRegister regNum, uint loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad32(this.fBoardNum, (int)regNum, loadValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo CLoad64(CounterRegister regNum, ulong loadValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbCLoad64(this.fBoardNum, (int)regNum, loadValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo DIn(DigitalPortType portNum, out ushort dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDIn_noncls(this.fBoardNum, (int)portNum, out dataValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo DOut(DigitalPortType portNum, ushort dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbDOut(this.fBoardNum, (int)portNum, dataValue);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		[Obsolete("Use MemRead which has ushort[] type for the dataBuffer parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ErrorInfo MemRead(out ushort dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemRead_noncls_Obsolete(this.fBoardNum, out dataBuffer, firstPoint, numPoints);
			}
			else
			{
				dataBuffer = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo MemRead(ushort[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemRead_noncls(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MemWrite which has ushort[] type for the dataBuffer parameter")]
		[CLSCompliant(false)]
		public ErrorInfo MemWrite(ref ushort dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemWrite_noncls_Obsolete(this.fBoardNum, ref dataBuffer, firstPoint, numPoints);
			}
			else
			{
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo MemWrite(ushort[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemWrite_noncls(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MemReadPretrig which has ushort[] type for the dataBuffer parameter")]
		[CLSCompliant(false)]
		public ErrorInfo MemReadPretrig(out ushort dataBuffer, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemReadPretrig_noncls_Obsolete(this.fBoardNum, out dataBuffer, firstPoint, numPoints);
			}
			else
			{
				dataBuffer = 0;
				this.m_errorInfo.ErrNumber = 10000;
			}
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo MemReadPretrig(ushort[] dataBuffer, int firstPoint, int numPoints)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbMemReadPretrig_noncls(this.fBoardNum, dataBuffer, firstPoint, numPoints);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo ToEngUnits(Range range, ushort dataVal, out float engUnits)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbToEngUnits(this.fBoardNum, (int)range, dataVal, out engUnits);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo ToEngUnits32(Range range, uint dataVal, out double engUnits)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbToEngUnits32(this.fBoardNum, (int)range, dataVal, out engUnits);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo FromEngUnits(Range range, float engUnits, out ushort dataVal)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbFromEngUnits_noncls(this.fBoardNum, (int)range, engUnits, out dataVal);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo SetTrigger(TriggerType trigType, ushort lowThreshold, ushort highThreshold)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetTrigger(this.fBoardNum, (int)trigType, lowThreshold, highThreshold);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo EnableEvent(EventType eventType, uint eventParameter, EventCallback callbackFunc, IntPtr userData)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbEnableEvent_noncls(this.fBoardNum, (uint)eventType, eventParameter, callbackFunc, userData);
			return this.m_errorInfo;
		}

		[CLSCompliant(false)]
		public ErrorInfo PulseOutStart(int timerNum, ref double frequency, ref double dutyCycle, uint pulseCount, ref double initialDelay, IdleState idleState, PulseOutOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbPulseOutStart(this.fBoardNum, timerNum, ref frequency, ref dutyCycle, pulseCount, ref initialDelay, (int)idleState, (int)options);
			return this.m_errorInfo;
		}

		internal ErrorInfo SetCalCoeff(FunctionType functionType, int channel, Range range, int item, int dataValue, OptionState store)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbSetCalCoeff(this.fBoardNum, (int)functionType, channel, (int)range, item, dataValue, (int)store);
			return this.m_errorInfo;
		}

		internal ErrorInfo GetCalCoeff(FunctionType functionType, int channel, Range range, int item, out int dataValue)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetCalCoeff(this.fBoardNum, (int)functionType, channel, (int)range, item, out dataValue);
			return this.m_errorInfo;
		}

		public ErrorInfo TEDSRead(int chan, byte[] dataBuffer, ref int count, TEDSReadOptions options)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbTEDSRead(this.fBoardNum, chan, dataBuffer, ref count, (int)options);
			return this.m_errorInfo;
		}
	}
}
