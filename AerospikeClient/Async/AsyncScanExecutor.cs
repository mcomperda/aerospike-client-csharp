/* 
 * Copyright 2012-2016 Aerospike, Inc.
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

namespace Aerospike.Client
{
	public sealed class AsyncScanExecutor : AsyncMultiExecutor
	{
		private readonly RecordSequenceListener listener;

		public AsyncScanExecutor
		(
			AsyncCluster cluster,
			ScanPolicy policy,
			RecordSequenceListener listener,
			string ns,
			string setName,
			string[] binNames
		)
		{
			this.listener = listener;

			Node[] nodes = cluster.Nodes;

			if (nodes.Length == 0)
			{
				throw new AerospikeException(ResultCode.SERVER_NOT_AVAILABLE, "Scan failed because cluster is empty.");
			}

			ulong taskId = RandomShift.ThreadLocalInstance.NextLong();

			// Create commands.
			AsyncScan[] tasks = new AsyncScan[nodes.Length];
			int count = 0;

			foreach (Node node in nodes)
			{
				tasks[count++] = new AsyncScan(this, cluster, (AsyncNode)node, policy, listener, ns, setName, binNames, taskId);
			}
			// Dispatch commands to nodes.
			Execute(tasks, policy.maxConcurrentNodes);
		}

		protected internal override void OnSuccess()
		{
			listener.OnSuccess();
		}

		protected internal override void OnFailure(AerospikeException ae)
		{
			listener.OnFailure(ae);
		}
	}
}
