using System.Windows.Forms;

namespace MccDaq
{
	public class ErrorInfo
	{
		public enum ErrorCode
		{
			NoErrors,
			BadBoard,
			DeadDigitalDev,
			DeadCounterDev,
			DeadDaDev,
			DeadAdDev,
			NotDigitalConf,
			NotCounterConf,
			NotDaConf,
			NotAdConf,
			NotMuxConf,
			BadPortNum,
			BadCounterDevNum,
			BadDaDevNum,
			BadSampleMode,
			BadInt,
			BadAdChan,
			BadCount,
			BadCntrConfig,
			BadDaVal,
			BadDaChan,
			AlreadyActive = 22,
			PageOverrun,
			BadRate,
			CompatMode,
			TrigState,
			AdStatusHung,
			TooFew,
			Overrun,
			BadRange,
			NoProgGain,
			BadFileName,
			DiskIsFull,
			CompatWarn,
			BadPointer,
			TooManyGains,
			RateWarning,
			ConvertDma,
			DtConnectErr,
			ForeContinuous,
			BadBoardType,
			WrongDigConfig,
			NotConfigurable,
			BadPortConfig,
			BadFirstPoint,
			EndOfFile,
			Not8254Ctr,
			Not9513Ctr,
			BadTrigType,
			BadTrigValue,
			BadOption = 52,
			BadPretrigCount,
			BadDivider = 55,
			BadSource,
			BadCompare,
			BadTimeOfDay,
			BadGateInterval,
			BadGateCntrl,
			BadCounterEdge,
			BadSpclGate,
			BadReload,
			BadRecycleFlag,
			BadBcdFlag,
			BadDirection,
			BadOutControl,
			BadBitNumber,
			NoneEnabled,
			BadCtrControl,
			BadExpChan,
			WrongAdRange,
			OutOfRange,
			BadTempScale,
			BadErrCode,
			NoQueue,
			ContinuousCount,
			Underrun,
			BadMemMode,
			FreqOverrun,
			NoCjcChan,
			BadChipNum,
			DigNotEnabled,
			Convert16Bits,
			NoMemBoard,
			DtActive,
			NotMemConf,
			OddChan,
			CtrNoInit,
			Not8536Ctr,
			FreeRunning,
			Interrupted,
			NoSelectors,
			NoBurstMode,
			NotWindowsFunc,
			NotSimulConf,
			EvenOddMismatch,
			M1RateWarning,
			NotRS485,
			NotDosFunc,
			RangeMismatch,
			ClockTooSlow,
			BadCalFactors,
			BadConfigType,
			BadConfigItem,
			NoPcmciaBoard,
			NoBackground,
			StringTooShort,
			ConvertExtMem,
			BadEuAdd,
			Das16JrRateWarning,
			Das08TooLowRate,
			AmbigSensorOnGp = 114,
			NoSensorTypeOnGp,
			NoConversionNeeded,
			NoExtContinuous,
			InvalidPretrigConvert,
			BadCtrReg,
			BadTrigThreshold,
			BadPcmSlotRef,
			AmbigPcmSlotRef,
			BadSensorType,
			DelBoardNotExist,
			NoBoardNameFile,
			CfgFileNotFound,
			NoVddInstalled,
			NoWindowsMemory,
			OutOfDosMemory,
			ObsoleteOption,
			NoPcmRegKey,
			NoCbul32Sys,
			NoDmaMememory,
			IrqNotAvailable,
			Not7266Ctr,
			BadQuadrature,
			BadCountMode,
			BadEncoding,
			BadIndexMode,
			BadInvertIndex,
			BadFlagPins,
			NoCtrStatus,
			NoGateAllowed,
			NoIndexAllowed,
			OpenConnection,
			BmContinuousCount,
			BadCallbackFunc,
			MbusInUse,
			MbusNoCtlr,
			BadEventType,
			AlreadyEnabled,
			BadEventSize,
			CantInstallEvent,
			BadBufferSize,
			BadAiMode,
			BadSignal,
			BadConnection,
			BadIndex,
			NoConnection,
			BadBurstIoCount,
			DeadDev,
			BadConfigVal,
			InvalidAccess,
			Unavailable,
			NotReady,
			OwnershipRefused,
			OwnershipFailed,
			BitUsedForAlarm = 169,
			PortUsedForAlarm,
			PacerOverRun,
			BadDebounceTime = 177,
			BadDebounceTrigMode,
			BadMappedCounter,
			BadCounterMode,
			BadTCChanMode,
			BadFrequency,
			BadEventParam,
			MismatchSetpointCount = 188,
			InvalidSetpointLevel,
			InvalidSetpointOutputType,
			InvalidSetpointOutputValue,
			InvalidSetpointLimits,
			StringTooLong,
			InvalidLogin,
			SessionInUse,
			NoExtPower,
			BadDutyCycle,
			BadInitialDelay = 199,
			InternalErr,
			CantLockDmaBuf,
			DmaInUse,
			BadMemHandle,
			NoMoreFiles = 344,
			BadFileNumber,
			InvalidStructSize,
			LossOfData,
			InvalidBinaryFile,
			InvalidDelimiter,
			InternalErr32 = 300,
			CfgFileReadFailure = 304,
			CfgFileWriteFailure,
			CfgFileCantOpen = 308,
			BadRtdConversion = 325,
			NoPciBios,
			BadPciIndex,
			NoPciBoard,
			CantInstallInt = 334,
			PcmciaErrs = 400,
			DosFileNotFound = 502,
			DosPathNotFound,
			DosReadFault = 530,
			WinCannotEnableInt = 603,
			WinCannotDisableInt = 605,
			WinCantPageLockBuffer,
			NoPcmCard = 630,
			InvalidGainArrayLength = 801,
			InvalidDimension0Length,
			NoTEDSSensor = 1000,
			InvalidTEDSSensor,
			CalibrationFailed,
			BitUsedForTerminalCountStatus,
			PortUsedForTerminalCountStatus,
			BadExcitation,
			BadBridgeType,
			BadLoadVal,
			BadTickSize,
			InvalidFunctionFor64bitPlatform = 10000
		}

		private const int ErrStrLen = 256;

		internal static ErrorReporting m_errorReporting = ErrorReporting.DontPrint;

		private ErrorCode f_ErrNumber;

		private string f_ErrString;

		private static bool f_IsLogToFile;

		public ErrorCode Value => this.f_ErrNumber;

		public string Message
		{
			get
			{
				if (this.f_ErrNumber == ErrorCode.InvalidFunctionFor64bitPlatform)
				{
					this.f_ErrString = "Invalid function for 64-bit platforms, Use the overloaded method";
				}
				else
				{
					CbwApi.Instance.cbGetErrMsg((int)this.f_ErrNumber, ref this.f_ErrString);
				}
				string text = this.f_ErrString;
				char[] trimChars = new char[1];
				return text.TrimEnd(trimChars);
			}
		}

		public bool LogToFile
		{
			get
			{
				return ErrorInfo.f_IsLogToFile;
			}
			set
			{
				ErrorInfo.f_IsLogToFile = value;
			}
		}

		internal int ErrNumber
		{
			set
			{
				this.f_ErrNumber = (ErrorCode)value;
				if (this.f_ErrNumber == ErrorCode.InvalidFunctionFor64bitPlatform && ErrorInfo.m_errorReporting != 0)
				{
					MessageBox.Show(this.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		public ErrorInfo()
			: this(0)
		{
		}

		public ErrorInfo(int errorNum)
		{
			ErrorInfo.f_IsLogToFile = false;
			this.f_ErrString = new string(new char[256]);
			this.SetError((ErrorCode)errorNum);
		}

		internal void SetError(ErrorCode errorCode)
		{
			this.f_ErrNumber = errorCode;
			if (ErrorInfo.f_IsLogToFile)
			{
				this.mLogToFile();
			}
		}

		private void mLogToFile()
		{
		}
	}
}
