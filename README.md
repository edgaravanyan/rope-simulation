# Unity Rope Physics

A Unity script for simulating rope physics using nodes and tension forces.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Parameters](#parameters)
- [License](#license)

## Introduction

This Unity script provides a simple implementation of rope physics using nodes and tension forces. It can be used for various applications where realistic rope-like behavior is required.

## Features

- Simulates rope physics with adjustable parameters.
- Easy integration into Unity projects.
- Visualization using LineRenderer component.

## Installation

1. Clone or download the repository.
2. Copy the `Rope.cs` script into your Unity project's `Scripts` folder.

## Usage

1. Attach the `Rope.cs` script to a GameObject in your Unity scene.
2. Adjust the script parameters in the Unity Inspector as needed.
3. Run your scene to see the rope simulation.

## Parameters

- `nodeCount`: Number of nodes in the rope.
- `nodeMass`: Mass of each node.
- `segmentLength`: Length of each rope segment.
- `gravity`: Gravitational force applied to the rope.
- `initialDamping`: Initial damping factor for each node.
- `maxDamping`: Maximum damping factor for each node.
- `dampingIncreaseRate`: Rate at which damping increases over time.
- `tension`: Tension factor affecting the rope's behavior.
- `lineWidth`: Width of the LineRenderer used for visualization.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT) - see the link for details.

