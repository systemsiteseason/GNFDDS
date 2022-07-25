using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFDLibrary.Textures.GNF;
using GFDLibrary.Textures.DDS;
using System.IO;

namespace GNFDDS
{
    internal class Program
    {
        static void Main(string[] args)
        {
			if (args.Length == 0 || args.Length == null)
				return;

			var path = Path.GetDirectoryName(args[0]);
			var nonext = Path.GetFileNameWithoutExtension(args[0]);

			var gnf = new GNFTexture(args[0]);


			bool cbPS4swiz = true;

			for (int i = 0; i < 278; i++)
			{
				pixbl[i] = 1;
			}
			for (int j = 70; j <= 84; j++)
			{
				pixbl[j] = 4;
			}
			for (int k = 94; k <= 99; k++)
			{
				pixbl[k] = 4;
			}

			FileStream fileStream = new FileStream(args[0], FileMode.Open, FileAccess.Read);
			new BinaryReader(fileStream);
			FileStream fileStream2 = new FileStream(Path.Combine(path, nonext) + ".dds", FileMode.Create);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
			long num = gnf.OffsetBitmap;
			fileStream.Seek(num, SeekOrigin.Begin);
			long num2 = fileStream.Length - num;
			if (num2 < 0L)
			{
			}
			int value = 1;
			int num3 = 808540228;
			int ddDXGI = 65;
			int num4 = pixbl[ddDXGI];
			int num5 = bpp[ddDXGI] * 2;
			if (num4 == 1)
			{
				num5 = bpp[ddDXGI] / 8;
			}
			if (ddDXGI == 71)
			{
				num3 = 827611204;
			}
			if (ddDXGI == 74)
			{
				num3 = 861165636;
			}
			if (ddDXGI == 77)
			{
				num3 = 894720068;
			}
			if (ddDXGI == 80)
			{
				num3 = 826889281;
			}
			if (ddDXGI == 83)
			{
				num3 = 843666497;
			}
			num2 = (long)(gnf.Width * gnf.Height * bpp[ddDXGI] / 8);
			binaryWriter.Write(533118272580L);
			binaryWriter.Write(4103);
			binaryWriter.Write(gnf.Height);
			binaryWriter.Write(gnf.Width);
			binaryWriter.Write((int)num2);
			binaryWriter.Write(0);
			binaryWriter.Write(value);
			fileStream2.Seek(44L, SeekOrigin.Current);
			binaryWriter.Write(32);
			binaryWriter.Write(4);
			binaryWriter.Write(num3);
			fileStream2.Seek(40L, SeekOrigin.Current);
			if (num3 == 808540228)
			{
				binaryWriter.Write(ddDXGI);
				binaryWriter.Write(3);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(0);
			}
			if (cbPS4swiz)
			{
				byte[] array = new byte[num2 * 2L];
				byte[] array2 = new byte[16];
				int num6 = gnf.Height / num4;
				int num7 = gnf.Width / num4;
				for (int i = 0; i < (num6 + 7) / 8; i++)
				{
					for (int j = 0; j < (num7 + 7) / 8; j++)
					{
						for (int k = 0; k < 64; k++)
						{
							int num8 = morton(k, 8, 8);
							int num9 = num8 / 8;
							int num10 = num8 % 8;
							fileStream.Read(array2, 0, num5);
							if (j * 8 + num10 < num7 && i * 8 + num9 < num6)
							{
								int destinationIndex = num5 * ((i * 8 + num9) * num7 + j * 8 + num10);
								Array.Copy(array2, 0, array, destinationIndex, num5);
							}
						}
					}
				}
				fileStream2.Write(array, 0, (int)num2);
			}
			else
			{
				byte[] buffer = new byte[num2];
				fileStream.Read(buffer, 0, (int)num2);
				fileStream2.Write(buffer, 0, (int)num2);
			}
			fileStream.Close();
			binaryWriter.Close();
			fileStream2.Close();
		}

		static int[] swi = new int[]
		{
			0,
			4,
			1,
			5,
			8,
			12,
			9,
			13,
			16,
			20,
			17,
			21,
			24,
			28,
			25,
			29,
			2,
			6,
			3,
			7,
			10,
			14,
			11,
			15,
			18,
			22,
			19,
			23,
			26,
			30,
			27,
			31
		};

		static int[] pixbl = new int[278];

		private static int[] bpp = new int[]
		{
			0,
			128,
			128,
			128,
			128,
			96,
			96,
			96,
			96,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			8,
			8,
			8,
			8,
			8,
			8,
			1,
			32,
			32,
			32,
			4,
			4,
			4,
			8,
			8,
			8,
			8,
			8,
			8,
			4,
			4,
			4,
			8,
			8,
			8,
			16,
			16,
			32,
			32,
			32,
			32,
			32,
			32,
			32,
			8,
			8,
			8,
			8,
			8,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			16
		};

		static int morton(int t, int sx, int sy)
		{
			int num2;
			int num = num2 = 1;
			int num3 = t;
			int num4 = sx;
			int num5 = sy;
			int num6 = 0;
			int num7 = 0;
			while (num4 > 1 || num5 > 1)
			{
				if (num4 > 1)
				{
					num6 += num2 * (num3 & 1);
					num3 >>= 1;
					num2 *= 2;
					num4 >>= 1;
				}
				if (num5 > 1)
				{
					num7 += num * (num3 & 1);
					num3 >>= 1;
					num *= 2;
					num5 >>= 1;
				}
			}
			return num7 * sx + num6;
		}


	}
}
