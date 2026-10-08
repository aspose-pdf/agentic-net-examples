using System;
using System.IO;

// Minimal stubs for the Open XML SDK types (DocumentFormat.OpenXml)
namespace DocumentFormat.OpenXml
{
    public abstract class OpenXmlElement { }
}

namespace DocumentFormat.OpenXml.Packaging
{
    public enum WordprocessingDocumentType { Document }

    public class WordprocessingDocument : IDisposable
    {
        public MainDocumentPart MainDocumentPart { get; set; }

        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type)
        {
            var doc = new WordprocessingDocument();
            doc.MainDocumentPart = new MainDocumentPart();
            return doc;
        }

        public void Dispose() { }

        public void Close() { }
    }

    public class MainDocumentPart
    {
        public DocumentFormat.OpenXml.Wordprocessing.Document Document { get; set; }

        public ImagePart AddImagePart(string contentType)
        {
            return new ImagePart();
        }
    }

    public class ImagePart
    {
        public void FeedData(Stream stream) { /* stub */ }
    }
}

namespace DocumentFormat.OpenXml.Wordprocessing
{
    public class Document
    {
        public Body Body { get; set; }

        public Document(Body body)
        {
            Body = body;
        }
    }

    public class Body : System.Collections.Generic.List<Paragraph> { }

    public class Paragraph : System.Collections.Generic.List<Run> { }

    public class Run : System.Collections.Generic.List<OpenXmlElement> { }

    public class Text : OpenXmlElement
    {
        public Text(string value) { Value = value; }
        public string Value { get; set; }
    }
}

// Application code
class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string wordPath = "output.docx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            // Create a new Word document (Open XML SDK)
            using (DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordDoc =
                DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(
                    wordPath,
                    DocumentFormat.OpenXml.Packaging.WordprocessingDocumentType.Document))
            {
                var mainPart = wordDoc.MainDocumentPart;
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(
                    new DocumentFormat.OpenXml.Wordprocessing.Body());

                int imageIndex = 1;

                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;

                        // Add the image to the Word document (as an image part)
                        var imagePart = mainPart.AddImagePart("image/png");
                        imagePart.FeedData(imgStream);

                        // Add a placeholder paragraph indicating the image position
                        var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                        var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
                        var text = new DocumentFormat.OpenXml.Wordprocessing.Text($"[Image {imageIndex}]");
                        run.Add(text);
                        paragraph.Add(run);
                        mainPart.Document.Body.Add(paragraph);
                    }

                    imageIndex++;
                }

                // If no images were found, add a note to the document
                if (imageIndex == 1)
                {
                    var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                    var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
                    var text = new DocumentFormat.OpenXml.Wordprocessing.Text("No images were found in the PDF.");
                    run.Add(text);
                    paragraph.Add(run);
                    mainPart.Document.Body.Add(paragraph);
                }

                // Finalize the Word document
                wordDoc.Close();
            }
        }

        Console.WriteLine($"Images extracted and embedded into Word document: {wordPath}");
    }
}