using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace JamSoft.AvaloniaUI.Dialogs.Helpers;

/// <summary>
/// A class of common file filters
/// </summary>
// https://developer.apple.com/library/archive/documentation/Miscellaneous/Reference/UTIRef/Articles/System-DeclaredUniformTypeIdentifiers.html
public static class CommonFilters
{
    /// <summary>
    /// Word files
    /// </summary>
    public static FilePickerFileType WordFilter = BuildFilter(
        "Word Files",  
        new[] { "*.docx", "*.doc" }, 
        new []{ "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document" }, 
        new []{ "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" });

    /// <summary>
    /// Excel files
    /// </summary>
    public static FilePickerFileType ExcelFilter = BuildFilter(
        "Excel Files", 
        new[] { "*.xlsx", "*.xls", "*.csv" }, 
        new []{"com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet", "public.comma-separated-values-text"}, 
        new []{"application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});

    /// <summary>
    /// HTML files
    /// </summary>
    public static FilePickerFileType HtmlFilter = BuildFilter(
        "HTML Files", 
        new[] { "*.html", "*.htm" }, 
        new []{"public.html"}, 
        new []{"text/html"});

    /// <summary>
    /// Txt Files
    /// </summary>
    public static FilePickerFileType TxtFilter = BuildFilter(
        "Text File", 
        new[] { "*.txt" }, 
        new []{"public.plain-text"}, 
        new []{"text/plain"});

    /// <summary>
    /// CSV Files
    /// </summary>
    public static FilePickerFileType CsvFilter = BuildFilter(
        "CSV File", 
        new[] { "*.csv" }, 
        new []{"public.comma-separated-values-text"}, 
        new []{"text/csv"});

    /// <summary>
    /// ZIP archive files
    /// </summary>
    public static FilePickerFileType ZipFilter = BuildFilter(
        "Zip File",
        new[] { "*.zip" }, 
        new []{"public.zip-archive"}, 
        new []{"application/zip"});
    
    /// <summary>
    /// Rar Files
    /// </summary>
    public static FilePickerFileType RarFilter = BuildFilter(
        "Rar File", 
        new[] { "*.rar" }, 
        new []{"com.rar-archive"}, 
        new []{"application/x-rar-compressed"});
    
    /// <summary>
    /// Rpm Files
    /// </summary>
    public static FilePickerFileType RpmFilter = BuildFilter(
        "Rpm File", 
        new[] { "*.rpm" }, 
        new []{"com.redhat.package-archive"}, 
        new []{"application/x-rpm"});
    
    /// <summary>
    /// DMG Files
    /// </summary>
    public static FilePickerFileType DmgFilter = BuildFilter(
        "Dmg File", 
        new[] { "*.dmg" }, 
        new []{"com.apple.disk-image"}, 
        new []{"application/x-apple-diskimage"});
    
    /// <summary>
    /// Tarball Files
    /// </summary>
    public static FilePickerFileType TarFilter = BuildFilter(
        "Tarball File", 
        new[] { "*.tar.gz" }, 
        new []{"public.tar-archive"}, 
        new []{"application/x-tar"});
    
    /// <summary>
    /// Iso Files
    /// </summary>
    public static FilePickerFileType IsoFilter = BuildFilter(
        "Iso File",
        new[] { "*.iso" }, 
        new []{"public.iso-image"}, 
        new []{"application/x-iso9660-image"});
    
    /// <summary>
    /// Wav Files
    /// </summary>
    public static FilePickerFileType WavFilter = BuildFilter(
        "Wav File", 
        new[] { "*.wav" }, 
        new []{"com.microsoft.waveform-audio"}, 
        new []{ "audio/wav", "audio/wave" });
    
    /// <summary>
    /// MP3 Files
    /// </summary>
    public static FilePickerFileType Mp3Filter = BuildFilter(
        "Mp3 File", new[] { "*.mp3" }, 
        new []{"public.mp3"}, 
        new []{"audio/mpeg", "audio/mpeg3", "audio/mpg", "audio/mp3", "audio/x-mpeg", "audio/x-mpeg3", " audio/x-mpg", "audio/x-mp3"});
    
    /// <summary>
    /// DLL Files
    /// </summary>
    public static FilePickerFileType DllFilter = BuildFilter(
        "Dll File", 
        new[] { "dll" }, 
        new []{"com.microsoft.windows-dynamic-link-library"}, 
        new []{"application/x-msdownload"});
    
    /// <summary>
    /// XML Files
    /// </summary>
    public static FilePickerFileType XmlFilter = BuildFilter(
        "XML File", 
        new[] { "*.xml" }, 
        new []{"public.xml"}, 
        new []{"text/xml", "application/xml"});
    
    /// <summary>
    /// Email Files
    /// </summary>
    public static FilePickerFileType EmailFilter = BuildFilter(
        "Email Files", 
        new[] { "*.eml", "*.email", "*.msg", "*.oft", "*.ost", "*.pst" }, 
        new []{"com.microsoft.outlook.email-message"}, 
        new []{"message/rfc822"});
    
    /// <summary>
    /// EXE Files
    /// </summary>
    public static FilePickerFileType ExeFilter = BuildFilter(
        "Windows Executable", 
        new[] { "*.exe" }, 
        new []{"com.microsoft.windows-executable", "application/vnd.microsoft.portable-executable" }, 
        new []{"application/x-msdownload"});
    
    /// <summary>
    /// Batch Files
    /// </summary>
    public static FilePickerFileType BatchFilter = BuildFilter(
        "Windows Batch File", 
        new[] { "*.bat" }, 
        new []{"com.microsoft.windows-batch"}, 
        new []{"application/x-msdownload"});
    
    /// <summary>
    /// TTF Font Files
    /// </summary>
    public static FilePickerFileType TtfFontFilter = BuildFilter(
        "TrueType Font File",
        new[] { "*.ttf" }, 
        new []{"public.truetype-ttf-font"},
        new []{"application/x-font-ttf"});
    
    /// <summary>
    /// Illustrator Files
    /// </summary>
    public static FilePickerFileType IllustratorFilter = BuildFilter(
        "Adobe Illustrator File", 
        new[] { "*.ai" }, 
        new []{"com.adobe.illustrator.ai-image"}, 
        new []{"application/illustrator"});
    
    /// <summary>
    /// Bitmap Files
    /// </summary>
    public static FilePickerFileType BitmapFilter = BuildFilter(
        "Bitmap Image", 
        new[] { "*.bmp" }, 
        new []{"com.microsoft.bmp"}, 
        new []{"image/bmp"} );
    
    /// <summary>
    /// GIF files
    /// </summary>
    public static FilePickerFileType GifFilter = BuildFilter(
        "GIF Image", 
        new[] { "*.gif" }, 
        new []{"com.compuserve.gif"}, 
        new []{"image/gif"});
    
    /// <summary>
    /// ICO Files
    /// </summary>
    public static FilePickerFileType IcoFilter = BuildFilter(
        "Icon File", 
        new[] { "*.ico" }, 
        new []{"com.microsoft.ico"}, 
        new []{"image/x-icon"});
    
    /// <summary>
    /// JPEG Files
    /// </summary>
    public static FilePickerFileType JpegFilter = BuildFilter(
        "JPEG Image", 
        new[] { "*.jpg", "*.jpeg" }, 
        new []{"public.jpeg"}, 
        new []{"image/jpeg"} );

    /// <summary>
    /// PNG Files
    /// </summary>
    public static FilePickerFileType PngFilter = BuildFilter(
        "PNG Image", 
        new[] { "*.png" }, 
        new []{"public.png"}, 
        new []{"image/png"});
    
    /// <summary>
    /// PostScript Files
    /// </summary>
    public static FilePickerFileType PostScriptFilter = BuildFilter(
        "PostScript File", 
        new[] { "*.ps" }, 
        new []{"com.adobe.postscript"}, 
        new []{"application/postscript"});
    
    /// <summary>
    /// PSD Files
    /// </summary>
    public static FilePickerFileType PsdFilter = BuildFilter(
        "Photoshop Image", 
        new[] { "*.psd" }, 
        new []{"com.adobe.photoshop-image"}, 
        new []{"image/vnd.adobe.photoshop"} );
    
    /// <summary>
    /// SVG Files
    /// </summary>
    public static FilePickerFileType SvgFilter = BuildFilter(
        "Scalable Vector Graphics File", 
        new[] { "*.svg" }, 
        new []{"public.svg-image"}, 
        new []{"image/svg+xml"} );
    
    /// <summary>
    /// TIFF Files
    /// </summary>
    public static FilePickerFileType TiffFilter = BuildFilter(
        "TIFF Image", 
        new[] { "*.tif", "*.tiff" }, 
        new []{"public.tiff"},
        new []{"image/tiff"} );
    
    /// <summary>
    /// WebP Files
    /// </summary>
    public static FilePickerFileType WebpFilter = BuildFilter(
        "WebP Image", 
        new[] { "*.webp" }, 
        new []{"public.webp"}, 
        new []{"image/webp"});
    
    /// <summary>
    /// Apple Keynote Files
    /// </summary>
    public static FilePickerFileType KeynoteFilter = BuildFilter(
        "Keynote Presentation",
        new[] { "*.key" }, 
        new []{"com.apple.keynote.key"}, 
        new []{"application/x-iwork-keynote-sffkey"});
    
    /// <summary>
    /// OpenOffice Impress Files
    /// </summary>
    public static FilePickerFileType OpenOfficeImpressFilter = BuildFilter(
        "OpenOffice Impress Presentation File", 
        new[] { "*.odp" }, 
        new []{"org.openoffice.presentation"}, 
        new []{"application/vnd.oasis.opendocument.presentation"});
    
    /// <summary>
    /// Powerpoint Slide Show Files
    /// </summary>
    public static FilePickerFileType PowerPointFilter = BuildFilter(
        "PowerPoint Slide Show", 
        new[]{ "*.pps" }, 
        new []{"com.microsoft.powerpoint.pps"}, 
        new []{"application/vnd.ms-powerpoint" });
    
    /// <summary>
    /// Powerpoint Presentation Files
    /// </summary>
    public static FilePickerFileType PowerpointFilter = BuildFilter(
        "PowerPoint Presentation", 
        new[] { "*.pptx", "*.ppt" }, 
        new []{"com.microsoft.powerpoint.ppt", "org.openxmlformats.presentationml.presentation"}, 
        new []{"application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation"});
    
    /// <summary>
    /// AVI Files
    /// </summary>
    public static FilePickerFileType AviFilter = BuildFilter(
        "AVI Video File", 
        new[] { "*.avi" }, 
        new []{"public.avi"}, 
        new []{"video/avi"} );
    
    /// <summary>
    /// MP4 Files
    /// </summary>
    public static FilePickerFileType Mp4Filter = BuildFilter(
        "MPEG4 Video File", 
        new[] { "*.mp4" }, 
        new []{"public.mpeg-4"}, 
        new []{"video/mp4"} );
    
    /// <summary>
    /// MPEG Files
    /// </summary>
    public static FilePickerFileType MpegFilter = BuildFilter(
        "MPEG Video File", 
        new[] { "mpg", "mpeg" }, 
        new []{"public.mpeg"}, 
        new []{"video/mpeg"});
    
    /// <summary>
    /// PDF Files
    /// </summary>
    public static FilePickerFileType PdfFilter = BuildFilter(
        "PDF File", 
        new[] { "*.pdf" }, 
        new []{"com.adobe.pdf"}, 
        new []{"application/pdf"});
    
    /// <summary>
    /// Rich Text Files
    /// </summary>
    public static FilePickerFileType RichTextFilter = BuildFilter(
        "Rich Text Format",
        new[] { "*.rtf" }, 
        new []{"public.rtf"}, 
        new []{"text/rtf"} );
    
    /// <summary>
    /// ODT Files
    /// </summary>
    public static FilePickerFileType OdtFilter = BuildFilter(
        "OpenOffice Writer Document File", 
        new[] { "*.odt" }, 
        new []{"org.openoffice.text"}, 
        new []{"application/vnd.oasis.opendocument.text"});

    /// <summary>
    /// Builds a filter instance
    /// </summary>
    /// <param name="name">The name of the filter</param>
    /// <param name="extensions">the array of file extensions</param>
    /// <param name="appleIds">the array of apple UTIs</param>
    /// <param name="mimeTypes">the array of mime types</param>
    /// <returns>a <see cref="FileDialogFilter"/> instance</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static FilePickerFileType BuildFilter(string name, string[] extensions, string[] appleIds, string[] mimeTypes)
    {
        return new FilePickerFileType(name)
        {
            Patterns = new List<string>(extensions), 
            AppleUniformTypeIdentifiers = new List<string>(appleIds), 
            MimeTypes = new List<string>(mimeTypes)
        };
    }
}