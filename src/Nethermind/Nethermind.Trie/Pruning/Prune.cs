// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

namespace Nethermind.Trie.Pruning
{
    public static class Prune
    {
        public static IPruningStrategy WhenCacheReaches(long dirtySizeInBytes)
            => new MemoryLimit(dirtySizeInBytes);

        public static IPruningStrategy WhenPersistedCacheReaches(this IPruningStrategy baseStrategy, long persistedMemoryLimit)
            => new PersistedMemoryLimit(baseStrategy, persistedMemoryLimit);

        public static IPruningStrategy TrackingPastKeys(this IPruningStrategy baseStrategy, int trackedPastKeyCount)
            => trackedPastKeyCount <= 0
                ? baseStrategy
                : new TrackedPastKeyCountStrategy(baseStrategy, trackedPastKeyCount);

        public static IPruningStrategy KeepingLastNState(this IPruningStrategy baseStrategy, int n)
            => new KeepLastNPruningStrategy(baseStrategy, n);

        public static IPruningStrategy WithDirtyNodeShardCount(this IPruningStrategy baseStrategy, int shardCount)
            => new ShardBitPruningStrategy(baseStrategy, shardCount);
    }
}
