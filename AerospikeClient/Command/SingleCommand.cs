/* 
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
namespace Aerospike.Client
{
	public abstract class SingleCommand : SyncCommand
	{
		private readonly Cluster cluster;
		protected internal readonly Key key;
		private readonly Partition partition;

		public SingleCommand(Cluster cluster, Key key)
		{
			this.cluster = cluster;
			this.key = key;
			this.partition = new Partition(key);
		}

		protected internal sealed override Node GetNode()
		{
			return cluster.GetNode(partition);
		}

		protected internal void EmptySocket(Connection conn)
		{
			// There should not be any more bytes.
			// Empty the socket to be safe.
			long sz = ByteUtil.BytesToLong(dataBuffer, 0);
			int headerLength = dataBuffer[8];
			int receiveSize = ((int)(sz & 0xFFFFFFFFFFFFL)) - headerLength;

			// Read remaining message bytes.
			if (receiveSize > 0)
			{
				SizeBuffer(receiveSize);
				conn.ReadFully(dataBuffer, receiveSize);
			}
		}
	}
}
