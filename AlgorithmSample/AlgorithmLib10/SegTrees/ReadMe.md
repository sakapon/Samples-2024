# Segment Trees
`SegTrees` namespaces

## Types
- RSQTree
- RAQTree
- MergeTree
- SplitTree

## Versions

### Length n
通常のセグメント木。
- 101 link-based
- 111 array-based

### Depth 32
- 103 link-based (recursion)
- 104 link-based
- 109 link-based (caching path)
- 114 array-based

### LCA
- 203 link-based (recursion)
- 204 link-based
- 214 array-based

## Implementation

### MaxBit
MaxBit 関数の代わりに [BitOperations.LeadingZeroCount メソッド](https://learn.microsoft.com/dotnet/api/system.numerics.bitoperations.leadingzerocount)を使う方法：

```cs
// 204
var f = 1 << 31 - BitOperations.LeadingZeroCount((uint)(node.L ^ key));
// 214
var f = 1 << 31 - BitOperations.LeadingZeroCount((uint)(li[node] ^ key));
```
