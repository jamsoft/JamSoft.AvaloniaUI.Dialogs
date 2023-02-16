using Avalonia.Controls;

namespace JamSoft.AvaloniaUI.Dialogs.Helpers;

/// <summary>
/// A class of common file filters
/// </summary>
public static class CommonFilters
{
    /// <summary>
    /// Word files
    /// </summary>
    public static FileDialogFilter WordFilter = BuildFilter("Word Files",  new[] { "docx", "doc" });

    /// <summary>
    /// Excel files
    /// </summary>
    public static FileDialogFilter ExcelFilter = BuildFilter("Excel Files", new[] { "xlsx", "xls", "csv" });

    /// <summary>
    /// HTML files
    /// </summary>
    public static FileDialogFilter HtmlFilter = BuildFilter("HTML Files", new[] { "html", "htm" });

    /// <summary>
    /// Txt Files
    /// </summary>
    public static FileDialogFilter TxtFilter = BuildFilter("Text File", new[] { "txt" });

    /// <summary>
    /// CSV Files
    /// </summary>
    public static FileDialogFilter CsvFilter = BuildFilter("CSV File", new[] { "csv" });

    /// <summary>
    /// ZIP archive files
    /// </summary>
    public static FileDialogFilter ZipFilter = BuildFilter("Zip File", new[] { "zip" });
    
    /// <summary>
    /// Rar Files
    /// </summary>
    public static FileDialogFilter RarFilter = BuildFilter("Rar File", new[] { "rar" });
    
    /// <summary>
    /// Rpm Files
    /// </summary>
    public static FileDialogFilter RpmFilter = BuildFilter("Rpm File", new[] { "rpm" });
    
    /// <summary>
    /// DMG Files
    /// </summary>
    public static FileDialogFilter DmgFilter = BuildFilter("Dmg File", new[] { "dmg" });
    
    /// <summary>
    /// Tarball Files
    /// </summary>
    public static FileDialogFilter TarFilter = BuildFilter("Tarball File", new[] { "tar.gz" });
    
    /// <summary>
    /// Iso Files
    /// </summary>
    public static FileDialogFilter IsoFilter = BuildFilter("Iso File",new[] { "iso" });
    
    /// <summary>
    /// Wav Files
    /// </summary>
    public static FileDialogFilter WavFilter = BuildFilter("Wav File", new[] { "wav" } );
    
    /// <summary>
    /// MP3 Files
    /// </summary>
    public static FileDialogFilter Mp3Filter = BuildFilter("Mp3 File", new[] { "mp3" } );
    
    /// <summary>
    /// DLL Files
    /// </summary>
    public static FileDialogFilter DllFilter = BuildFilter("Dll File", new[] { "dll" });
    
    /// <summary>
    /// XML Files
    /// </summary>
    public static FileDialogFilter XmlFilter = BuildFilter("XML File", new[] { "xml" });
    
    /// <summary>
    /// Email Files
    /// </summary>
    public static FileDialogFilter EmailFilter = BuildFilter("Email Files", new[] { "eml", "email", "msg", "oft", "ost", "pst" });
    
    /// <summary>
    /// EXE Files
    /// </summary>
    public static FileDialogFilter ExeFilter = BuildFilter("Windows Executable", new[] { "exe" });
    
    /// <summary>
    /// Batch Files
    /// </summary>
    public static FileDialogFilter BatchFilter = BuildFilter("Windows Batch File", new[] { "bat" });
    
    /// <summary>
    /// TTF Font Files
    /// </summary>
    public static FileDialogFilter TtfFontFilter = BuildFilter("TrueType Font File", new[] { "ttf" });
    
    /// <summary>
    /// Illustrator Files
    /// </summary>
    public static FileDialogFilter IllustratorFilter = BuildFilter("Adobe Illustrator File", new[] { "ai" });
    
    /// <summary>
    /// Bitmap Files
    /// </summary>
    public static FileDialogFilter BitmapFilter = BuildFilter("Bitmap Image", new[] { "bmp" });
    
    /// <summary>
    /// GIF files
    /// </summary>
    public static FileDialogFilter GifFilter = BuildFilter("GIF Image", new[] { "gif" });
    
    /// <summary>
    /// ICO Files
    /// </summary>
    public static FileDialogFilter IcoFilter = BuildFilter("Icon File", new[] { "ico" });
    
    /// <summary>
    /// JPEG Files
    /// </summary>
    public static FileDialogFilter JpegFilter = BuildFilter("JPEG Image", new[] { "jpg", "jpeg" } );

    /// <summary>
    /// PNG Files
    /// </summary>
    public static FileDialogFilter PngFilter = BuildFilter("PNG Image", new[] { "png" });
    
    /// <summary>
    /// PostScript Files
    /// </summary>
    public static FileDialogFilter PostScriptFilter = BuildFilter("PostScript File", new[] { "ps" });
    
    /// <summary>
    /// PSD Files
    /// </summary>
    public static FileDialogFilter PsdFilter = BuildFilter("Photoshop Image", new[] { "psd" });
    
    /// <summary>
    /// SVG Files
    /// </summary>
    public static FileDialogFilter SvgFilter = BuildFilter("Scalable Vector Graphics File", new[] { "svg" });
    
    /// <summary>
    /// TIFF Files
    /// </summary>
    public static FileDialogFilter TiffFilter = BuildFilter("TIFF Image", new[] { "tif", "tiff" });
    
    /// <summary>
    /// WebP Files
    /// </summary>
    public static FileDialogFilter WebpFilter = BuildFilter("WebP Image", new[] { "webp" });
    
    /// <summary>
    /// Apple Keynote Files
    /// </summary>
    public static FileDialogFilter KeynoteFilter = BuildFilter("Keynote Presentation", new[] { "key" } );
    
    /// <summary>
    /// OpenOffice Impress Files
    /// </summary>
    public static FileDialogFilter OpenOfficeImpressFilter = BuildFilter("OpenOffice Impress Presentation File", new[] { "odp" });
    
    /// <summary>
    /// Powerpoint Slide Show Files
    /// </summary>
    public static FileDialogFilter PowerPointFilter = BuildFilter("PowerPoint Slide Show", new[]{ "pps" });
    
    /// <summary>
    /// Powerpoint Presentation Files
    /// </summary>
    public static FileDialogFilter PowerpointFilter = BuildFilter("PowerPoint Presentation", new[] { "pptx", "ppt" });
    
    /// <summary>
    /// AVI Files
    /// </summary>
    public static FileDialogFilter AviFilter = BuildFilter("AVI Video File", new[] { "avi" });
    
    /// <summary>
    /// MP4 Files
    /// </summary>
    public static FileDialogFilter Mp4Filter = BuildFilter("MPEG4 Video File", new[] { "mp4" });
    
    /// <summary>
    /// MPEG Files
    /// </summary>
    public static FileDialogFilter MpegFilter = BuildFilter("MPEG Video File", new[] { "mpg", "mpeg" } );
    
    /// <summary>
    /// PDF Files
    /// </summary>
    public static FileDialogFilter PdfFilter = BuildFilter("PDF File", new[] { "pdf" });
    
    /// <summary>
    /// Rich Text Files
    /// </summary>
    public static FileDialogFilter RichTextFilter = BuildFilter("Rich Text Format", new[] { "rtf" });
    
    /// <summary>
    /// ODT Files
    /// </summary>
    public static FileDialogFilter OdtFilter = BuildFilter("OpenOffice Writer Document File", new[] { "odt" });
    
    /// <summary>
    /// Builds a filter instance
    /// </summary>
    /// <param name="name">The name of the filter</param>
    /// <param name="extensions">the array of file extensions</param>
    /// <returns>a <see cref="FileDialogFilter"/> instance</returns>
    public static FileDialogFilter BuildFilter(string name, string[] extensions)
    {
        return new FileDialogFilter { Name = name, Extensions = new List<string>(extensions) };
    }
}