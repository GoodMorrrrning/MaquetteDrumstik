using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
    //
    // RSDeserializeClass.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class Stream
    {
        public int profile { get; set; }
        public string quality { get; set; }
        public string id { get; set; }
        public int fps { get; set; }
    }

    public class AkfireInterconnectQuic
    {
        public string url { get; set; }
        public string origin { get; set; }
        public string avc_url { get; set; }
    }

    public class FastlySkyfire
    {
        public string url { get; set; }
        public string origin { get; set; }
        public string avc_url { get; set; }
    }

    public class Cdns
    {
        public AkfireInterconnectQuic akfire_interconnect_quic { get; set; }
        public FastlySkyfire fastly_skyfire { get; set; }
    }

    public class StreamsAvc
    {
        public int profile { get; set; }
        public string quality { get; set; }
        public string id { get; set; }
        public int fps { get; set; }
    }

    public class Dash
    {
        public bool separate_av { get; set; }
        public List<Stream> streams { get; set; }
        public Cdns cdns { get; set; }
        public List<StreamsAvc> streams_avc { get; set; }
        public string default_cdn { get; set; }
    }

    public class AkfireInterconnectQuic2
    {
        public string url { get; set; }
        public string origin { get; set; }
        public string avc_url { get; set; }
    }

    public class FastlySkyfire2
    {
        public string url { get; set; }
        public string origin { get; set; }
        public string avc_url { get; set; }
    }

    public class Cdns2
    {
        public AkfireInterconnectQuic2 akfire_interconnect_quic { get; set; }
        public FastlySkyfire2 fastly_skyfire { get; set; }
    }

    public class Hls
    {
        public bool separate_av { get; set; }
        public string default_cdn { get; set; }
        public Cdns2 cdns { get; set; }
    }

    public class Progressive
    {
        public int profile { get; set; }
        public int width { get; set; }
        public string mime { get; set; }
        public int fps { get; set; }
        public string url { get; set; }
        public string cdn { get; set; }
        public string quality { get; set; }
        public string id { get; set; }
        public string origin { get; set; }
        public int height { get; set; }
    }

    public class Files
    {
        public Dash dash { get; set; }
        public Hls hls { get; set; }
        public List<Progressive> progressive { get; set; }
    }

    public class Sentry
    {
        public string url { get; set; }
        public bool enabled { get; set; }
        public bool debug_enabled { get; set; }
        public int debug_intent { get; set; }
    }

    public class Data
    {
    }

    public class Chromecast
    {
        public bool track { get; set; }
        public Data data { get; set; }
        public bool group { get; set; }
    }

    public class Data2
    {
    }

    public class StatsFresnel
    {
        public bool track { get; set; }
        public Data2 data { get; set; }
        public bool group { get; set; }
    }

    public class Data3
    {
    }

    public class Webvr
    {
        public bool track { get; set; }
        public Data3 data { get; set; }
        public bool group { get; set; }
    }

    public class AbTests
    {
        public Chromecast chromecast { get; set; }
        public StatsFresnel stats_fresnel { get; set; }
        public Webvr webvr { get; set; }
    }

    public class GcDebug
    {
        public string bucket { get; set; }
    }

    public class Client
    {
        public string ip { get; set; }
    }

    public class Cookie
    {
        public int scaling { get; set; }
        public double volume { get; set; }
        public object quality { get; set; }
        public int hd { get; set; }
        public object captions { get; set; }
    }

    public class Build
    {
        public string backend { get; set; }
        public string js { get; set; }
    }

    public class Urls
    {
        public string barebone_js { get; set; }
        public string test_imp { get; set; }
        public string js_base { get; set; }
        public string fresnel { get; set; }
        public string js { get; set; }
        public string proxy { get; set; }
        public string mux_url { get; set; }
        public string fresnel_mimir_inputs_url { get; set; }
        public string fresnel_chunk_url { get; set; }
        public string three_js { get; set; }
        public string vuid_js { get; set; }
        public string fresnel_manifest_url { get; set; }
        public string chromeless_css { get; set; }
        public string player_telemetry_url { get; set; }
        public string chromeless_js { get; set; }
        public string css { get; set; }
    }

    public class Flags
    {
        public int dnt { get; set; }
        public string preload_video { get; set; }
        public int plays { get; set; }
        public int partials { get; set; }
        public int autohide_controls { get; set; }
    }

    public class Hevc
    {
        public List<object> hdr { get; set; }
        public List<object> sdr { get; set; }
    }

    public class FileCodecs
    {
        public Hevc hevc { get; set; }
        public List<object> av1 { get; set; }
        public List<string> avc { get; set; }
    }

    public class Request
    {
        public Files files { get; set; }
        public string lang { get; set; }
        public Sentry sentry { get; set; }
        public AbTests ab_tests { get; set; }
        public object referrer { get; set; }
        public string cookie_domain { get; set; }
        public int timestamp { get; set; }
        public GcDebug gc_debug { get; set; }
        public int expires { get; set; }
        public Client client { get; set; }
        public string currency { get; set; }
        public string session { get; set; }
        public Cookie cookie { get; set; }
        public Build build { get; set; }
        public Urls urls { get; set; }
        public string signature { get; set; }
        public Flags flags { get; set; }
        public string country { get; set; }
        public FileCodecs file_codecs { get; set; }
    }

    public class Available
    {
        public int is_current { get; set; }
        public int id { get; set; }
        public long file_id { get; set; }
    }

    public class Version
    {
        public object current { get; set; }
        public List<Available> available { get; set; }
    }

    public class Thumbs
    {
        public string a1280 { get; set; }
        public string a960 { get; set; }
        public string a640 { get; set; }
        public string @base { get; set; }
    }

    public class Owner
    {
        public string account_type { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string img_2x { get; set; }
        public int id { get; set; }
    }

    public class Video
    {
        public Version Version { get; set; }
        public int height { get; set; }
        public int duration { get; set; }
        public Thumbs thumbs { get; set; }
        public Owner owner { get; set; }
        public int id { get; set; }
        public string embed_code { get; set; }
        public string title { get; set; }
        public string share_url { get; set; }
        public int width { get; set; }
        public string embed_permission { get; set; }
        public double fps { get; set; }
        public int spatial { get; set; }
        public object live_event { get; set; }
        public int allow_hd { get; set; }
        public int hd { get; set; }
        public object lang { get; set; }
        public int default_to_hd { get; set; }
        public string url { get; set; }
        public string privacy { get; set; }
        public string bypass_token { get; set; }
        public object unlisted_hash { get; set; }
    }

    public class User
    {
        public int team_origin_user_id { get; set; }
        public int liked { get; set; }
        public string account_type { get; set; }
        public string vimeo_api_client_token { get; set; }
        public object vimeo_api_interaction_tokens { get; set; }
        public int team_id { get; set; }
        public int watch_later { get; set; }
        public int owner { get; set; }
        public int id { get; set; }
        public bool mod { get; set; }
        public int logged_in { get; set; }
    }

    public class Settings
    {
        public int fullscreen { get; set; }
        public int byline { get; set; }
        public int like { get; set; }
        public int playbar { get; set; }
        public int title { get; set; }
        public int color { get; set; }
        public int speed { get; set; }
        public int watch_later { get; set; }
        public int share { get; set; }
        public int scaling { get; set; }
        public int spatial_compass { get; set; }
        public int collections { get; set; }
        public int info_on_pause { get; set; }
        public int portrait { get; set; }
        public int logo { get; set; }
        public int embed { get; set; }
        public int badge { get; set; }
        public int spatial_label { get; set; }
        public int volume { get; set; }
    }

    public class Embed
    {
        public int autopause { get; set; }
        public int playsinline { get; set; }
        public Settings settings { get; set; }
        public string color { get; set; }
        public string texttrack { get; set; }
        public int on_site { get; set; }
        public string app_id { get; set; }
        public int muted { get; set; }
        public int dnt { get; set; }
        public string player_id { get; set; }
        public object api { get; set; }
        public bool editor { get; set; }
        public string context { get; set; }
        public int time { get; set; }
        public string outro { get; set; }
        public int log_plays { get; set; }
        public object quality { get; set; }
        public int transparent { get; set; }
        public int loop { get; set; }
        public int autoplay { get; set; }
    }

    public class RSDeserializeClass
    {
        public string cdn_url { get; set; }
        public string vimeo_api_url { get; set; }
        public Request request { get; set; }
        public string player_url { get; set; }
        public Video video { get; set; }
        public User user { get; set; }
        public Embed embed { get; set; }
        public int view { get; set; }
        public string vimeo_url
        {
            get; set;
        }

    }
}
