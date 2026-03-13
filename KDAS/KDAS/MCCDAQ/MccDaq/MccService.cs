using System;
using System.ComponentModel;

namespace MccDaq
{
	public class MccService
	{
		public const float CurrentRevNum = 5.5f;

		public const int GetFirst = -2;

		public const int GetNext = -3;

		public const int BoardNameLen = 25;

		public static ErrorInfo m_errorInfo = new ErrorInfo();

		private MccService()
		{
		}

		public static ErrorInfo GetRevision(out float revNum, out float vxdRevNum)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetRevision(out revNum, out vxdRevNum);
			return MccService.m_errorInfo;
		}

		internal static ErrorInfo LoadConfig(string cfgFileName)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbLoadConfig(cfgFileName);
			return MccService.m_errorInfo;
		}

		internal static ErrorInfo SaveConfig(string cfgFileName)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbSaveConfig(cfgFileName);
			return MccService.m_errorInfo;
		}

		public static ErrorInfo ErrHandling(ErrorReporting errorReporting, ErrorHandling errorHandling)
		{
			int num = CbwApi.Instance.cbErrHandling((int)errorReporting, (int)errorHandling);
			MccService.m_errorInfo.ErrNumber = num;
			if (num == 0)
			{
				ErrorInfo.m_errorReporting = errorReporting;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo FileGetInfo(string fileName, out short lowChan, out short highChan, out int pretrigCount, out int totalCount, out int rate, out Range range)
		{
			int num;
			int errNumber = CbwApi.Instance.cbFileGetInfo(fileName, out lowChan, out highChan, out pretrigCount, out totalCount, out rate, out num);
			range = (Range)num;
			MccService.m_errorInfo.ErrNumber = errNumber;
			return MccService.m_errorInfo;
		}

		[Obsolete("Use FileRead which has short[] type for the dataBuffer parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ErrorInfo FileRead(string fileName, int firstPoint, ref int numPoints, out short dataBuffer)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileRead_Obsolete(fileName, firstPoint, ref numPoints, out dataBuffer);
			}
			else
			{
				dataBuffer = 0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo FileRead(string fileName, int firstPoint, ref int numPoints, short[] dataBuffer)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileRead(fileName, firstPoint, ref numPoints, dataBuffer);
			return MccService.m_errorInfo;
		}

		public static ErrorInfo FileRead(string fileName, int firstPoint, ref int numPoints, out double[,] dataBuffer, int numChannels)
		{
			short[] array = new short[numPoints];
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileRead(fileName, firstPoint, ref numPoints, array);
			int num = numPoints / numChannels;
			dataBuffer = new double[numChannels, num];
			for (int i = 0; i < numChannels; i++)
			{
				for (int j = 0; j < num; j++)
				{
					double[,] obj = dataBuffer;
					int num2 = i;
					int num3 = j;
					double num4 = Convert.ToDouble((ushort)array[i + j * numChannels]);
					obj[num2, num3] = num4;
				}
			}
			return MccService.m_errorInfo;
		}

		[Obsolete("Use WinBufToArray which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ErrorInfo WinBufToArray(int memHandle, out short dataArray, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray_Obsolete(memHandle, out dataArray, firstPoint, numPoints);
			}
			else
			{
				dataArray = 0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinBufToArray(IntPtr memHandle, short[] dataArray, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray(memHandle, dataArray, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[Obsolete("Use WinBufToArray which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ErrorInfo WinBufToArray(int memHandle, out double[,] dataArray, int firstPoint, int numPoints, int numChannels)
		{
			if (IntPtr.Size == 4)
			{
				short[] array = new short[numPoints * numChannels];
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray_Obsolete(memHandle, out array[0], firstPoint, numPoints * numChannels);
				dataArray = new double[numChannels, numPoints];
				for (int i = 0; i < numChannels; i++)
				{
					for (int j = 0; j < numPoints; j++)
					{
						double[,] obj = dataArray;
						int num = i;
						int num2 = j;
						double num3 = Convert.ToDouble((ushort)array[i + j * numChannels]);
						obj[num, num2] = num3;
					}
				}
			}
			else
			{
				dataArray = null;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinBufToArray(IntPtr memHandle, double[,] dataArray, int firstPoint, int numPoints, int numChannels)
		{
			short[] array = new short[numPoints * numChannels];
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray(memHandle, array, firstPoint, numPoints * numChannels);
			dataArray = new double[numChannels, numPoints];
			for (int i = 0; i < numChannels; i++)
			{
				for (int j = 0; j < numPoints; j++)
				{
					double[,] array2 = dataArray;
					int num = i;
					int num2 = j;
					double num3 = Convert.ToDouble((ushort)array[i + j * numChannels]);
					array2[num, num2] = num3;
				}
			}
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufToArray32 which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinBufToArray32(int memHandle, out int dataArray, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32_Obsolete(memHandle, out dataArray, firstPoint, numPoints);
			}
			else
			{
				dataArray = 0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinBufToArray32(IntPtr memHandle, int[] dataArray, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32(memHandle, dataArray, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ScaledWinBufToArray which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo ScaledWinBufToArray(int memHandle, out double dataArray, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbScaledWinBufToArray_Obsolete(memHandle, out dataArray, firstPoint, numPoints);
			}
			else
			{
				dataArray = 0.0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo ScaledWinBufToArray(IntPtr memHandle, double[] dataArray, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbScaledWinBufToArray(memHandle, dataArray, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufToArray32 which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinBufToArray32(int memHandle, out double[,] dataArray, int firstPoint, int numPoints, int numChannels)
		{
			if (IntPtr.Size == 4)
			{
				int[] array = new int[numPoints * numChannels];
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32_Obsolete(memHandle, out array[0], firstPoint, numPoints * numChannels);
				dataArray = new double[numChannels, numPoints];
				for (int i = 0; i < numChannels; i++)
				{
					for (int j = 0; j < numPoints; j++)
					{
						double[,] obj = dataArray;
						int num = i;
						int num2 = j;
						double num3 = Convert.ToDouble((uint)array[i + j * numChannels]);
						obj[num, num2] = num3;
					}
				}
			}
			else
			{
				dataArray = null;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinBufToArray32(IntPtr memHandle, double[,] dataArray, int firstPoint, int numPoints, int numChannels)
		{
			int[] array = new int[numPoints * numChannels];
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32(memHandle, array, firstPoint, numPoints * numChannels);
			dataArray = new double[numChannels, numPoints];
			for (int i = 0; i < numChannels; i++)
			{
				for (int j = 0; j < numPoints; j++)
				{
					double[,] array2 = dataArray;
					int num = i;
					int num2 = j;
					double num3 = Convert.ToDouble((uint)array[i + j * numChannels]);
					array2[num, num2] = num3;
				}
			}
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinArrayToBuf which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinArrayToBuf(ref short dataArray, int memHandle, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf_Obsolete(ref dataArray, memHandle, firstPoint, numPoints);
			}
			else
			{
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinArrayToBuf(short[] dataArray, IntPtr memHandle, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf(dataArray, memHandle, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[Obsolete("Use ScaledWinArrayToBuf which has IntPtr type for the memHandle parameter")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ErrorInfo ScaledWinArrayToBuf(ref double dataArray, int memHandle, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbScaledWinArrayToBuf_Obsolete(ref dataArray, memHandle, firstPoint, numPoints);
			}
			else
			{
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo ScaledWinArrayToBuf(double[] dataArray, IntPtr memHandle, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbScaledWinArrayToBuf(dataArray, memHandle, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinArrayToBuf which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinArrayToBuf(ref double[,] dataArray, int memHandle, int firstPoint, int numPoints, int numChannels)
		{
			if (IntPtr.Size == 4)
			{
				short[] array = new short[numPoints * numChannels];
				for (int i = 0; i < numChannels; i++)
				{
					for (int j = 0; j < numPoints; j++)
					{
						array[i + j * numChannels] = Convert.ToInt16((short)dataArray[i, j]);
					}
				}
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf_Obsolete(ref array[0], memHandle, firstPoint, numPoints * numChannels);
			}
			else
			{
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinArrayToBuf(double[,] dataArray, IntPtr memHandle, int firstPoint, int numPoints, int numChannels)
		{
			short[] array = new short[numPoints * numChannels];
			for (int i = 0; i < numChannels; i++)
			{
				for (int j = 0; j < numPoints; j++)
				{
					array[i + j * numChannels] = Convert.ToInt16((short)dataArray[i, j]);
				}
			}
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf(array, memHandle, firstPoint, numPoints * numChannels);
			return MccService.m_errorInfo;
		}

		[Obsolete("Use WinBufAllocEx instead")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int WinBufAlloc(int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				return CbwApi.Instance.cbWinBufAlloc_Obsolete(numPoints);
			}
			return 0;
		}

		public static IntPtr WinBufAllocEx(int numPoints)
		{
			return CbwApi.Instance.cbWinBufAlloc(numPoints);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufAlloc32Ex instead")]
		public static int WinBufAlloc32(int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				return CbwApi.Instance.cbWinBufAlloc32_Obsolete(numPoints);
			}
			return 0;
		}

		public static IntPtr WinBufAlloc32Ex(int numPoints)
		{
			return CbwApi.Instance.cbWinBufAlloc32(numPoints);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufAllo64Ex instead")]
		public static int WinBufAlloc64(int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				return CbwApi.Instance.cbWinBufAlloc64_Obsolete(numPoints);
			}
			return 0;
		}

		public static IntPtr WinBufAlloc64Ex(int numPoints)
		{
			return CbwApi.Instance.cbWinBufAlloc64(numPoints);
		}

		[Obsolete("Use ScaledWinBufAlloc instead")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int ScaledWinBufAlloc(int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				return CbwApi.Instance.cbScaledWinBufAlloc_Obsolete(numPoints);
			}
			return 0;
		}

		public static IntPtr ScaledWinBufAllocEx(int numPoints)
		{
			return CbwApi.Instance.cbScaledWinBufAlloc(numPoints);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufFreeEx instead")]
		public static ErrorInfo WinBufFree(int memHandle)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFree_Obsolete(memHandle);
			}
			else
			{
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		public static ErrorInfo WinBufFreeEx(IntPtr memHandle)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufFree(memHandle);
			return MccService.m_errorInfo;
		}

		public static ErrorInfo DeclareRevision(ref float revNum)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbDeclareRevision(ref revNum);
			return MccService.m_errorInfo;
		}

		public static ErrorInfo GetBoardName(int BoardNumber, ref string boardName)
		{
			string text = new string(' ', 25);
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbGetBoardName(BoardNumber, ref text);
			if (MccService.m_errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				text = text.TrimEnd();
				int startIndex = text.Length - 1;
				boardName = text.Remove(startIndex, 1);
			}
			return MccService.m_errorInfo;
		}

		[Obsolete("Use FileRead which has ushort[] type for the dataBuffer parameter")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ErrorInfo FileRead(string fileName, int firstPoint, ref int numPoints, out ushort dataBuffer)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileRead_noncls_Obsolete(fileName, firstPoint, ref numPoints, out dataBuffer);
			}
			else
			{
				dataBuffer = 0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		[CLSCompliant(false)]
		public static ErrorInfo FileRead(string fileName, int firstPoint, ref int numPoints, ushort[] dataBuffer)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbFileRead_noncls(fileName, firstPoint, ref numPoints, dataBuffer);
			return MccService.m_errorInfo;
		}

		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufToArray which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinBufToArray(int memHandle, out ushort dataArray, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray_noncls(memHandle, out dataArray, firstPoint, numPoints);
			}
			else
			{
				dataArray = 0;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		[CLSCompliant(false)]
		public static ErrorInfo WinBufToArray(IntPtr memHandle, ushort[] dataArray, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray_noncls(memHandle, dataArray, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WinBufToArray32 which has IntPtr type for the memHandle parameter")]
		[CLSCompliant(false)]
		public static ErrorInfo WinBufToArray32(int memHandle, out uint dataArray, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32_noncls_Obsolete(memHandle, out dataArray, firstPoint, numPoints);
			}
			else
			{
				dataArray = 0u;
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		[CLSCompliant(false)]
		public static ErrorInfo WinBufToArray32(IntPtr memHandle, uint[] dataArray, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinBufToArray32_noncls(memHandle, dataArray, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		[Obsolete("Use WinArrayToBuf which has IntPtr type for the memHandle parameter")]
		public static ErrorInfo WinArrayToBuf(ref ushort dataArray, int memHandle, int firstPoint, int numPoints)
		{
			if (IntPtr.Size == 4)
			{
				MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf_noncls_Obsolete(ref dataArray, memHandle, firstPoint, numPoints);
			}
			else
			{
				MccService.m_errorInfo.ErrNumber = 10000;
			}
			return MccService.m_errorInfo;
		}

		[CLSCompliant(false)]
		public static ErrorInfo WinArrayToBuf(ushort[] dataArray, IntPtr memHandle, int firstPoint, int numPoints)
		{
			MccService.m_errorInfo.ErrNumber = CbwApi.Instance.cbWinArrayToBuf_noncls(dataArray, memHandle, firstPoint, numPoints);
			return MccService.m_errorInfo;
		}
	}
}
