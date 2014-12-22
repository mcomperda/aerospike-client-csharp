﻿/* 
 * Copyright 2012-2014 Aerospike, Inc.
 *
 * Portions may be licensed to Aerospike, Inc. under one or more contributor
 * license agreements.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
using System;
using System.Threading;

namespace Aerospike.Demo
{
	/// <summary>
	/// Generate random numbers using xorshift128plus algorithm.
	/// This class is not thread-safe and should be instantiated once per thread.
	/// </summary>
	public sealed class RandomShift
	{
		private static int seed = Environment.TickCount;
		
		private ulong seed0;
		private ulong seed1;

		/// <summary>
		/// Generate seeds using standard Random class.
		/// </summary>
		public RandomShift()
		{
			Random random = new Random(Interlocked.Increment(ref seed));
			byte[] bytes = new byte[8];

			random.NextBytes(bytes);
			seed0 = BitConverter.ToUInt64(bytes, 0);
			
			random.NextBytes(bytes);
			seed1 = BitConverter.ToUInt64(bytes, 0);
		}

		/// <summary>
		/// Generate random bytes.
		/// </summary>
		public void NextBytes(byte[] bytes)
		{
			int len = bytes.Length;
			int i = 0;

			while (i < len)
			{
				ulong r = NextLong();
				int n = Math.Min(len - i, 8);

				for (int j = 0; j < n; j++)
				{
					bytes[i++] = (byte)r;
					r >>= 8;
				}
			}
		}

		/// <summary>
		/// Generate random integer between begin (inclusive) and end (exclusive).
		/// </summary>
		public int Next(int begin, int end)
		{
			ulong b = (ulong)begin;
			ulong e = (ulong)end;
			return (int)((NextLong() % (e - b)) + b);
		}

		/// <summary>
		/// Generate random unsigned integer.
		/// </summary>
		public uint Next()
		{
			return (uint)NextLong();
		}

		/// <summary>
		/// Generate random unsigned long value.
		/// </summary>
		public ulong NextLong()
		{
			ulong s1 = seed0;
			ulong s0 = seed1;
			seed0 = s0;
			s1 ^= s1 << 23;
			seed1 = (s1 ^ s0 ^ (s1 >> 17) ^ (s0 >> 26));
			return seed1 + s0;
		}
	}
}
