using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class ExtractionConfig
{
    public bool ExtractText { get; set; }
    public bool ExtractImages { get; set; }
    public bool ExtractAttachments { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPdf = "input.pdf";
        const string configPath = "config.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        ExtractionConfig config;
        using (FileStream configStream = File.OpenRead(configPath))
        {
            config = JsonSerializer.Deserialize<ExtractionConfig>(configStream);
        }

        if (config == null)
        {
            Console.Error.WriteLine("Failed to read configuration.");
            return;
        }

        using (Document doc = new Document(inputPdf))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(doc);

            if (config.ExtractText)
            {
                extractor.ExtractTextMode = 0; // pure text mode
                extractor.ExtractText();
                extractor.GetText("extracted.txt");
                Console.WriteLine("Text extracted to extracted.txt");
            }

            if (config.ExtractImages)
            {
                extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imageFile = $"image_{imageIndex}.png";
                    extractor.GetNextImage(imageFile);
                    Console.WriteLine($"Image extracted to {imageFile}");
                    imageIndex++;
                }
            }

            if (config.ExtractAttachments)
            {
                extractor.ExtractAttachment();
                // GetAttachNames returns IList<string>; use the interface directly or convert to array.
                IList<string> attachmentNames = extractor.GetAttachNames();
                foreach (string name in attachmentNames)
                {
                    extractor.GetAttachment(name);
                    Console.WriteLine($"Attachment extracted to {name}");
                }
            }
        }
    }
}
