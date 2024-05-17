```

BenchmarkDotNet v0.13.12, Pop!_OS 22.04 LTS
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.202
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


```

| Method                |     Mean |    Error |   StdDev |
|-----------------------|---------:|---------:|---------:|
| RunIsUserJuntinUnique | 371.6 μs |  7.26 μs |  9.69 μs |
| RunIsUserJuntin       | 512.6 μs | 10.24 μs | 26.25 μs |
