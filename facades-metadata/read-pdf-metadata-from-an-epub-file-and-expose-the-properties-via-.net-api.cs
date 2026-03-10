using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace EbookMetadata
{
    // Simple DTO to hold common PDF metadata fields
    public class PdfMetadata
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        // Aspose.Pdf.Facades.PdfFileInfo exposes dates as strings, so keep them as strings here
        public string CreationDate { get; set; }
        public string ModDate { get; set; }
        public int NumberOfPages { get; set; }
        public string Producer { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsPdfFile { get; set; }
    }

    public static class EpubMetadataReader
    {
        /// <summary>
        /// Loads an EPUB file, converts it to a PDF document in memory,
        /// and extracts the PDF metadata using Aspose.Pdf.Facades.PdfFileInfo.
        /// </summary>
        /// <param name="epubPath">Full path to the source EPUB file.</param>
        /// <returns>Populated PdfMetadata instance.</returns>
        public static PdfMetadata Read(string epubPath)
        {
            if (string.IsNullOrWhiteSpace(epubPath))
                throw new ArgumentException("EPUB path must be provided.", nameof(epubPath));

            if (!File.Exists(epubPath))
                throw new FileNotFoundException("EPUB file not found.", epubPath);

            // Load EPUB as a PDF document using EpubLoadOptions
            using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
            {
                // PdfFileInfo can be constructed directly from a Document instance
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfDoc))
                {
                    // Populate the DTO with properties exposed by PdfFileInfo
                    var meta = new PdfMetadata
                    {
                        Title = fileInfo.Title,
                        Author = fileInfo.Author,
                        Creator = fileInfo.Creator,
                        Subject = fileInfo.Subject,
                        Keywords = fileInfo.Keywords,
                        CreationDate = fileInfo.CreationDate,
                        ModDate = fileInfo.ModDate,
                        NumberOfPages = fileInfo.NumberOfPages,
                        Producer = fileInfo.Producer,
                        IsEncrypted = fileInfo.IsEncrypted,
                        IsPdfFile = fileInfo.IsPdfFile
                    };

                    return meta;
                }
            }
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            const string epubFile = "sample.epub";

            try
            {
                PdfMetadata metadata = EpubMetadataReader.Read(epubFile);

                Console.WriteLine($"Title          : {metadata.Title}");
                Console.WriteLine($"Author         : {metadata.Author}");
                Console.WriteLine($"Creator        : {metadata.Creator}");
                Console.WriteLine($"Subject        : {metadata.Subject}");
                Console.WriteLine($"Keywords       : {metadata.Keywords}");
                Console.WriteLine($"Creation Date  : {metadata.CreationDate}");
                Console.WriteLine($"Modification   : {metadata.ModDate}");
                Console.WriteLine($"Pages          : {metadata.NumberOfPages}");
                Console.WriteLine($"Producer       : {metadata.Producer}");
                Console.WriteLine($"Encrypted      : {metadata.IsEncrypted}");
                Console.WriteLine($"Valid PDF file : {metadata.IsPdfFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading EPUB metadata: {ex.Message}");
            }
        }
    }
}
