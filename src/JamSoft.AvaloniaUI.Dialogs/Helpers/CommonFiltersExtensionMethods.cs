using Avalonia.Platform.Storage;

namespace JamSoft.AvaloniaUI.Dialogs.Helpers;

/// <summary>
/// A class containing extension methods for <see cref="FilePickerFileType"/> to allow for easier merging of file types.
/// </summary>
public static class CommonFiltersExtensionMethods
{
    /// <summary>
    /// Merges the specified file type with the other file types.
    /// </summary>
    /// <param name="fileType"></param>
    /// <param name="others"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static FilePickerFileType MergeWith(this FilePickerFileType fileType, FilePickerFileType[] others, string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            for(int i = 0;i< others.Length;i++)
            {
                if (i < others.Length - 1)
                {
                    fileType.MergeWith(others[i], name);
                }
                else
                {
                    fileType.MergeWith(others[i]);
                }
            }
        }
        
        foreach (var ft in others)
        {
            fileType.MergeWith(ft);
        }

        return fileType;
    }
    
    /// <summary>
    /// merges the specified file type with the other file type.
    /// </summary>
    /// <param name="fileType"></param>
    /// <param name="other"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static FilePickerFileType MergeWith(this FilePickerFileType fileType, FilePickerFileType? other, string? name = null)
    {
        if (other == null)
            return fileType;
        
        if (fileType.Patterns != null)
        {
            if (other.Patterns != null)
            {
                fileType.Patterns = fileType.Patterns.Concat(other.Patterns).ToArray();
            }
        }
        else
        {
            fileType.Patterns = other.Patterns;    
        }
        
        if (fileType.AppleUniformTypeIdentifiers != null)
        {
            if (other.AppleUniformTypeIdentifiers != null)
            {
                fileType.AppleUniformTypeIdentifiers = fileType.AppleUniformTypeIdentifiers.Concat(other.AppleUniformTypeIdentifiers).ToArray();
            }
        }
        else
        {
            fileType.AppleUniformTypeIdentifiers = other.AppleUniformTypeIdentifiers;    
        }
        
        if (fileType.MimeTypes != null)
        {
            if (other.MimeTypes != null)
            {
                fileType.MimeTypes = fileType.MimeTypes.Concat(other.MimeTypes).ToArray();
            }
        }
        else
        {
            fileType.MimeTypes = other.MimeTypes;    
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            return new FilePickerFileType(name)
            {
                Patterns = fileType.Patterns,
                AppleUniformTypeIdentifiers = fileType.AppleUniformTypeIdentifiers,
                MimeTypes = fileType.MimeTypes
            };
        }

        return fileType;
    }
}