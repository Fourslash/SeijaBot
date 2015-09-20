using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using SKYPE4COMLib;
using System.IO;
using Twitch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Net.Http;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;

namespace SkypeBot2
{
    public class DbInfo
    {
        public DbInfo()
        { }
        public string id { get; set; }
        public string created_at { get; set; }
        public string uploader_id { get; set; }
        public string score { get; set; }
        public string source { get; set; }
        public string md5 { get; set; }
        public string last_comment_bumped_at { get; set; }
        public string rating { get; set; }
        public string image_width { get; set; }
        public string image_height { get; set; }
        public string tag_string { get; set; }
        public string is_note_locked { get; set; }
        public string fav_count { get; set; }
        public string file_ext { get; set; }
        public string parent_id { get; set; }
        public string has_children { get; set; }
        public string approver_id { get; set; }
        public string tag_count_general { get; set; }
        public string tag_count_artist { get; set; }
        public string tag_count_character { get; set; }
        public string is_status_locked { get; set; }
        public string file_size { get; set; }
        public string fav_string { get; set; }
        public string pool_string { get; set; }
        public string up_score { get; set; }
        public string down_score { get; set; }
        public string is_pending { get; set; }
        public string is_flagged { get; set; }
        public string is_deleted { get; set; }
        public string tag_count { get; set; }
        public string updated_at { get; set; }
        public string is_banned { get; set; }
        public string pixiv_id { get; set; }
        public string last_commented_at { get; set; }
        public string has_active_children { get; set; }
        public string bit_flags { get; set; }

        public string uploader_name { get; set; }
        public string has_large { get; set; }
        public string tag_string_artist { get; set; }
        public string tag_string_character { get; set; }
        public string tag_string_copyright { get; set; }
        public string tag_string_general { get; set; }
        public string has_visible_children { get; set; }
        public string file_url { get; set; }
        public string large_file_url { get; set; }
        public string preview_file_url { get; set; }

    }
    public class DbTag
    {
        public DbTag()
        { }
        public string id { get; set; }
        public string name { get; set; }
        public string post_count { get; set; }
        public string related_tags { get; set; }
        public string related_tags_updated_at { get; set; }
        public string category { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string is_locked { get; set; }

    }
}