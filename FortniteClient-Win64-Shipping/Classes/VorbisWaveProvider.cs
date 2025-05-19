using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using NAudio.Wave;
using NVorbis;

public class VorbisWaveProvider : IWaveProvider
{
    private readonly VorbisReader _vorbisReader;
    private readonly WaveFormat _waveFormat;

    public VorbisWaveProvider(VorbisReader vorbisReader)
    {
        _vorbisReader = vorbisReader;
        _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_vorbisReader.SampleRate, _vorbisReader.Channels);
    }

    public WaveFormat WaveFormat => _waveFormat;

    public int Read(byte[] buffer, int offset, int count)
    {
        int samplesRequested = count / 4;
        float[] floatBuffer = new float[samplesRequested];

        int samplesRead = _vorbisReader.ReadSamples(floatBuffer, 0, samplesRequested);

        if (samplesRead == 0)
            return 0;

        Buffer.BlockCopy(floatBuffer, 0, buffer, offset, samplesRead * 4);
        return samplesRead * 4;
    }
}
