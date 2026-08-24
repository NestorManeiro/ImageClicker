# Image Detector

A lightweight C#/.NET application that detects images on the screen using OpenCV template matching and performs configurable mouse clicks when a match is found.

## Features

* Screen-wide or custom detection regions
* OpenCV-based image recognition
* Configurable detection threshold
* Randomized scan intervals
* Randomized delay before clicking
* Randomized delay after clicking
* Random click position within the detected image
* Physical mouse clicks
* Background window clicks
* Configurable automatic breaks
* Debug logging
* Support for PNG, JPG, JPEG, and BMP images
* Multi-monitor support through the Windows virtual screen

## Requirements

* Windows
* .NET
* OpenCvSharp

## How It Works

1. Add the images you want to detect to the configured image folder.
2. Start the detector.
3. The application captures the screen at randomized intervals.
4. Each configured image is checked using OpenCV template matching.
5. When an image matches the configured threshold, the application waits for the configured pre-click delay.
6. A click is performed at a randomized position within the detected image.
7. The configured post-click delay is applied.
8. The next scan starts after the configured scan interval.

Each image can produce at most one match per screen scan.

## Configuration

The application allows you to configure:

* Image folder
* Detection threshold
* Minimum and maximum scan interval
* Minimum and maximum pre-click delay
* Minimum and maximum post-click delay
* Click position margins
* Break intervals
* Break duration
* Background clicking
* Debug mode

## Supported Image Formats

* `.png`
* `.jpg`
* `.jpeg`
* `.bmp`

## Project

Built with:

* C#
* .NET
* Windows Forms
* OpenCvSharp
* OpenCV

## License

Add your preferred license here.
