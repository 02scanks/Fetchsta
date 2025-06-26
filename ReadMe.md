# Fetchsta

**Fetchsta** is a fast, terminal-based download manager written in C# (.NET) for Linux. It supports downloading files with live progress.

In the future I will be adding flexible CLI flags, and extensible features like multi-threaded downloads and resumable transfers.

---

## Features

- Terminal-based, no GUI overhead
- Real-time download progress bar
- Smart speed meter (KB/s or MB/s)
- Custom output directory
- Lightweight `.deb` package for easy install

---

## Installation

### Option 1: From `.deb` Package

Download the latest release:

```bash
wget https://github.com/02scanks/Fetchsta/releases/download/release1.0.0/fetchsta_1.0.0.deb
sudo dpkg -i fetchsta_1.0.0.deb
```

### Option 2: Build yourself from project files

```bash
git clone https://github.com/02scanks/fetchsta.git
cd fetchsta
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -o publish
sudo cp publish/fetchsta /usr/local/bin/
chmod +x /usr/local/bin/fetchsta
```

## Preferences
On first launch, Fetchsta creates a config file at:
```bash
~/.fetchsta/config.json
```
You can preconfigure a default download path or other settings here.

