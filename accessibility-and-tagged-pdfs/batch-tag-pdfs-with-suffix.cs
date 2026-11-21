using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // Suffix to append to processed file names
        const string suffix = "_tagged";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Enable global auto‑tagging; it will run when TaggedContent is accessed
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Access tagged content (auto‑tagging occurs if the document is not already tagged)
                    ITaggedContent tagged = doc.TaggedContent;

                    // Optionally set language and title metadata
                    tagged.SetLanguage("en-US");
                    tagged.SetTitle(Path.GetFileNameWithoutExtension(pdfPath));

                    // Obtain the root structure element
                    StructureElement root = tagged.RootElement;

                    // If the document has no structure children, add a simple paragraph element
                    if (root.ChildElements.Count == 0)
                    {
                        ParagraphElement para = tagged.CreateParagraphElement();
                        para.SetText("Document content (auto‑tagged).");
                        root.AppendChild(para); // AppendChild with single argument
                    }

                    // Build output file name with the required suffix
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string outPath = Path.Combine(outputFolder, $"{fileName}{suffix}.pdf");

                    // Save the modified PDF (no PreSave required)
                    doc.Save(outPath);
                    Console.WriteLine($"Processed: {pdfPath} -> {outPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}