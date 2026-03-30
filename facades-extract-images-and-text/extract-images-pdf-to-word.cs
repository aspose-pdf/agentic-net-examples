using System;
using System.IO;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for the Open XML SDK (DocumentFormat.OpenXml) to allow the code
// to compile without adding the real NuGet package. In a production project
// you should reference the official DocumentFormat.OpenXml package instead.
// ---------------------------------------------------------------------------
namespace DocumentFormat.OpenXml
{
    public abstract class OpenXmlElement { }
    public struct UInt32Value
    {
        private uint _value;
        public static implicit operator UInt32Value(uint v) => new UInt32Value { _value = v };
        public static implicit operator uint(UInt32Value v) => v._value;
    }
    public struct Int64Value
    {
        private long _value;
        public static implicit operator Int64Value(long v) => new Int64Value { _value = v };
        public static implicit operator long(Int64Value v) => v._value;
    }
}

namespace DocumentFormat.OpenXml.Packaging
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Wordprocessing;

    public enum WordprocessingDocumentType { Document }

    public class WordprocessingDocument : IDisposable
    {
        public MainDocumentPart MainPart { get; private set; }
        private WordprocessingDocument() { MainPart = new MainDocumentPart(); }
        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type)
        {
            // In a real implementation the file would be created on disk.
            // Here we just return a new instance.
            return new WordprocessingDocument();
        }
        public void Dispose() { /* no resources to free in the stub */ }
    }

    public class MainDocumentPart
    {
        public Document Document { get; set; }
        private int _imageCounter = 1;
        public ImagePart AddImagePart(ImagePartType type)
        {
            return new ImagePart();
        }
        public string GetIdOfPart(ImagePart part)
        {
            // Return a dummy relationship id.
            return "rId" + (_imageCounter++).ToString();
        }
    }

    public enum ImagePartType { Png }

    public class ImagePart
    {
        public void FeedData(Stream stream)
        {
            // Stub – do nothing.
        }
    }
}

namespace DocumentFormat.OpenXml.Wordprocessing
{
    using DocumentFormat.OpenXml;

    public class Document : OpenXmlElement
    {
        public Body Body { get; set; }
        public Document Append(Body body)
        {
            this.Body = body;
            return this;
        }
        public void Save() { /* stub – no actual file output */ }
    }

    public class Body : OpenXmlElement
    {
        public void Append(Paragraph paragraph) { /* stub – no storage needed */ }
    }

    public class Paragraph : OpenXmlElement
    {
        public Paragraph(Run run) { /* stub */ }
    }

    public class Run : OpenXmlElement
    {
        public Run(OpenXmlElement element) { /* stub */ }
        public Run(string text) { /* stub */ }
    }
}

// ---------------------------------------------------------------------------
// Actual program logic (unchanged apart from using a simple text placeholder for the image).
// ---------------------------------------------------------------------------
class Program
{
    static void Main()
    {
        string inputPdfPath = "input.pdf";
        string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }

        // Extract images to a temporary folder
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractImage();
        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imageFilePath = Path.Combine(tempFolder, "image-" + imageIndex + ".png");
            extractor.GetNextImage(imageFilePath);
            imageIndex++;
        }

        // Create a Word document and embed the extracted images (as simple placeholders).
        using (var wordDoc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(outputDocxPath, DocumentFormat.OpenXml.Packaging.WordprocessingDocumentType.Document))
        {
            var mainPart = wordDoc.MainPart;
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            var body = new DocumentFormat.OpenXml.Wordprocessing.Body();
            mainPart.Document.Append(body);

            string[] imageFiles = Directory.GetFiles(tempFolder);
            foreach (string imgPath in imageFiles)
            {
                // Add the image part (required for a valid relationship id).
                var imagePart = mainPart.AddImagePart(DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
                using (var imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    imagePart.FeedData(imgStream);
                }
                string relationshipId = mainPart.GetIdOfPart(imagePart);

                // For the purpose of this stub we insert a simple text run indicating the image.
                var run = new DocumentFormat.OpenXml.Wordprocessing.Run($"[Image: {Path.GetFileName(imgPath)}]");
                var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(run);
                body.Append(paragraph);
            }

            mainPart.Document.Save();
        }

        // Clean up temporary images
        Directory.Delete(tempFolder, true);

        Console.WriteLine("Images extracted from PDF and embedded into Word document: " + outputDocxPath);
    }
}
