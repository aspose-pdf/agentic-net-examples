using System;
using System.IO;
using Aspose.Pdf.Facades;

public class BookmarkModifier
{
    public static byte[] ModifyBookmarks(byte[] pdfBytes, string sourceTitle, string newTitle)
    {
        if (pdfBytes == null)
        {
            throw new ArgumentNullException(nameof(pdfBytes));
        }

        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the PDF from the input stream
            bookmarkEditor.BindPdf(inputStream);

            // Modify bookmark titles
            bookmarkEditor.ModifyBookmarks(sourceTitle, newTitle);

            // Save the modified PDF to an output stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                bookmarkEditor.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

// Added entry point to satisfy the executable project requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // Simple demonstration of the BookmarkModifier.
        // Expected arguments: <inputPdfPath> <sourceTitle> <newTitle>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdfPath> <sourceTitle> <newTitle>");
            return;
        }

        string inputPath = args[0];
        string sourceTitle = args[1];
        string newTitle = args[2];

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        byte[] modifiedPdf = BookmarkModifier.ModifyBookmarks(pdfBytes, sourceTitle, newTitle);

        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                         Path.GetFileNameWithoutExtension(inputPath) + "_modified.pdf");
        File.WriteAllBytes(outputPath, modifiedPdf);
        Console.WriteLine($"Modified PDF saved to: {outputPath}");
    }
}