# Dataweave

A .NET 8 library for extracting rich media metadata using [ffprobe](https://ffmpeg.org/ffprobe.html), with a simple C# API.

## Features

- Runs `ffprobe` as a subprocess and parses its JSON output.
- Extracts video, audio, subtitle streams, chapters, and format information.
- Configurable probe parameters: analyzeduration, probesize, unit, sexagesimal, bitexact.
- Strongly-typed metadata model for easy consumption in .NET applications.

## Requirements

- .NET 8.0 or later
- [ffprobe](https://ffmpeg.org/ffprobe.html) must be installed and available in your system PATH.

## Installation

You can install the `Dataweave` library using the NuGet Package Manager

## Code Examples

### Basic Usage
```
using Dataweave;
string filePath = "sample.mp4"; 
var ffprobe = new FFprobe(filePath);
var metadata = ffprobe.Metadata;
foreach (var video in metadata.Streams.Video) 
{ 
	Console.WriteLine($"Codec: {video.CodecName}, Resolution: {video.Width}x{video.Height}"); 
}
```