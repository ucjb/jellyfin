﻿using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MediaBrowser.Model.Dto
{
    public class MediaSourceInfo
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public string Container { get; set; }
        public long? Size { get; set; }

        public LocationType LocationType { get; set; }

        public string Name { get; set; }

        public long? RunTimeTicks { get; set; }

        public VideoType? VideoType { get; set; }

        public IsoType? IsoType { get; set; }

        public Video3DFormat? Video3DFormat { get; set; }

        public List<MediaStream> MediaStreams { get; set; }

        public List<string> Formats { get; set; }

        public int? Bitrate { get; set; }

        public TransportStreamTimestamp? Timestamp { get; set; }

        public MediaSourceInfo()
        {
            Formats = new List<string>();
            MediaStreams = new List<MediaStream>();
        }

        public int? DefaultAudioStreamIndex { get; set; }
        public int? DefaultSubtitleStreamIndex { get; set; }

        [IgnoreDataMember]
        public MediaStream DefaultAudioStream
        {
            get
            {
                if (DefaultAudioStreamIndex.HasValue)
                {
                    var val = DefaultAudioStreamIndex.Value;

                    foreach (MediaStream i in MediaStreams)
                    {
                        if (i.Type == MediaStreamType.Audio && i.Index == val)
                        {
                            return i;
                        }
                    }
                }

                foreach (MediaStream i in MediaStreams)
                {
                    if (i.Type == MediaStreamType.Audio && i.IsDefault)
                    {
                        return i;
                    }
                }

                foreach (MediaStream i in MediaStreams)
                {
                    if (i.Type == MediaStreamType.Audio)
                    {
                        return i;
                    }
                }

                return null;
            }
        }

        [IgnoreDataMember]
        public MediaStream VideoStream
        {
            get
            {
                foreach (MediaStream i in MediaStreams)
                {
                    if (i.Type == MediaStreamType.Video && (i.Codec ?? string.Empty).IndexOf("jpeg", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        return i;
                    }
                }

                return null;
            }
        }
    }
}