﻿using NAudio.Wave;
using NVorbis;
using System.Windows.Resources;
using System.Windows;
using System.IO;
using FortniteClient_Win64_Shipping.Classes;

public static class AudioPlayer
{
    private static WaveOutEvent _outputDevice;
    private static VorbisReader _vorbisReader;
    private static Stream _audioStream;
    private static VolumeWaveProvider16 _volumeProvider;

    public static void PlayTimedLoop(string packUri, float volume = 1.0f)
    {
        try
        {
            Uri uri = new Uri(packUri, UriKind.Absolute);
            var resourceInfo = Application.GetResourceStream(uri);
            if (resourceInfo == null)
                return;

            var vorbisReader = new VorbisReader(resourceInfo.Stream, false);
            var waveProvider = new VorbisWaveProvider(vorbisReader);
            var wave16Provider = new WaveFloatTo16Provider(waveProvider);
            var volumeProvider = new VolumeWaveProvider16(wave16Provider) { Volume = volume };

            var outputDevice = new WaveOutEvent();
            outputDevice.Init(volumeProvider);

            outputDevice.PlaybackStopped += (s, e) =>
            {
                outputDevice.Dispose();
                vorbisReader.Dispose();
            };

            outputDevice.Play();
        }
        catch(Exception ex) 
        {
            Logger.Log($"Failed to play sound effect! An exception occured. | {ex}");
        }
    }

    public static void PlayMusic(string packUri, float volume = 1.0f, bool loop = false)
    {
        try
        {
            StopMusic();

            Uri uri = new Uri(packUri, UriKind.Absolute);
            var resourceInfo = Application.GetResourceStream(uri);
            if (resourceInfo == null) return;

            _audioStream = resourceInfo.Stream;

            _vorbisReader = new VorbisReader(_audioStream, false);
            var waveProvider = new VorbisWaveProvider(_vorbisReader);
            var wave16Provider = new WaveFloatTo16Provider(waveProvider);
            _volumeProvider = new VolumeWaveProvider16(wave16Provider) { Volume = volume };
            _outputDevice = new WaveOutEvent();
            _outputDevice.Init(_volumeProvider);

            if (loop)
            {
                _outputDevice.PlaybackStopped += (s, e) =>
                {
                    if (_vorbisReader != null && _outputDevice != null)
                    {
                        _vorbisReader.DecodedPosition = 0;
                        _outputDevice.Play();
                    }
                };
            }

            _outputDevice.Play();
        }
        catch(Exception ex) 
        {
            Logger.Log($"Failed to play music! {ex}");
        }
    }
    public static void StopMusic()
    {
        try
        {
            if (_outputDevice != null)
            {
                _outputDevice.Stop();
                _outputDevice.Dispose();
                _outputDevice = null;
            }

            if (_vorbisReader != null)
            {
                _vorbisReader.Dispose();
                _vorbisReader = null;
            }

            if (_audioStream != null)
            {
                _audioStream.Dispose();
                _audioStream = null;
            }
        }
        catch(Exception ex)
        {
            Logger.Log($"An exception occured while trying to stop background music. | {ex}");
        }
    }

    public static async Task FadeOutMusic(float durationSeconds = 2.0f)
    {
        try
        {
            if (_outputDevice == null || _volumeProvider == null || _outputDevice.PlaybackState != PlaybackState.Playing)
                return;

            float initialVolume = _volumeProvider.Volume;
            int steps = 30;
            float stepAmount = initialVolume / steps;
            int delay = (int)(durationSeconds * 1000 / steps);

            for (int i = 0; i < steps; i++)
            {
                _volumeProvider.Volume = Math.Max(0, _volumeProvider.Volume - stepAmount);
                await Task.Delay(delay);
            }

            StopMusic();
        }
        catch(Exception ex) 
        {
            Logger.Log($"Cannot fade out music. | {ex}");
        }
    }
}