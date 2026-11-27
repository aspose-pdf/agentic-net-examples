using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices; // for JPEG conversion

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "superscript_subscript.txt";
        const string manifestJpegPath = "manifest.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and extract superscript / subscript fragments
        StringBuilder extractedBuilder = new StringBuilder();

        Document srcDoc = new Document(inputPdfPath);
        foreach (Page page in srcDoc.Pages)
        {
            // Use TextFragmentAbsorber to get individual fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            page.Accept(absorber);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The IsSuperscript / IsSubscript properties may not exist in older versions.
                // Use reflection to safely check for them.
                bool isSuperscript = false;
                bool isSubscript = false;
                var stateType = fragment.TextState.GetType();
                var supProp = stateType.GetProperty("IsSuperscript");
                var subProp = stateType.GetProperty("IsSubscript");
                if (supProp != null && supProp.PropertyType == typeof(bool))
                    isSuperscript = (bool)supProp.GetValue(fragment.TextState);
                if (subProp != null && subProp.PropertyType == typeof(bool))
                    isSubscript = (bool)subProp.GetValue(fragment.TextState);

                if (isSuperscript || isSubscript)
                {
                    extractedBuilder.AppendLine(fragment.Text);
                }
            }
        }

        // Save extracted text to a .txt file
        File.WriteAllText(outputTxtPath, extractedBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Extracted text saved to '{outputTxtPath}'.");

        // Create a simple PDF containing the extracted text
        using (Document manifestDoc = new Document())
        {
            Page page = manifestDoc.Pages.Add();

            TextFragment manifestFragment = new TextFragment(extractedBuilder.ToString())
            {
                TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
            };
            page.Paragraphs.Add(manifestFragment);

            // Convert the first page of the PDF to a JPEG image using JpegDevice
            JpegDevice jpegDevice = new JpegDevice(300); // 300 DPI
            jpegDevice.Process(manifestDoc.Pages[1], manifestJpegPath);
        }

        Console.WriteLine($"JPEG manifest saved to '{manifestJpegPath}'.");
    }
}
