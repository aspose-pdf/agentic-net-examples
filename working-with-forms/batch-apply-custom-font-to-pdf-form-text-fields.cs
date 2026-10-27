using System;
using System.IO;
using System.Drawing; // kept for potential drawing needs; ambiguous types are fully qualified below
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class BatchFontApplier
{
    static void Main()
    {
        // Network folder containing the PDFs
        const string sourceFolder = @"\\SERVER\Shared\PDFs";
        // Optional: folder to write the modified PDFs (can be the same as sourceFolder)
        const string outputFolder = @"\\SERVER\Shared\PDFs\Processed";

        // Path to the custom TrueType font to apply
        const string customFontPath = @"C:\Fonts\CustomFont.ttf";

        // Verify the font file exists
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Font file not found: {customFontPath}");
            return;
        }

        // Load the custom font once – fully qualify the Aspose.Pdf.Text.Font type to avoid ambiguity
        Aspose.Pdf.Text.Font customFont = Aspose.Pdf.Text.FontRepository.OpenFont(customFontPath);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over all form fields
                    foreach (var field in doc.Form.Fields)
                    {
                        // Apply only to text box fields (including regular text fields)
                        if (field is TextBoxField textField)
                        {
                            // Set the default appearance for the field so that existing and newly entered text uses the custom font
                            // Font size and colour can be adjusted as required
                            textField.DefaultAppearance = new DefaultAppearance(
                                customFont,               // Font object loaded from the TTF file (Aspose.Pdf.Text.Font)
                                12,                      // Desired font size
                                System.Drawing.Color.Black); // System.Drawing.Color is required by the constructor
                        }
                    }

                    // Determine output path (overwrite original or write to separate folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                    // Save the modified PDF (Document.Save writes PDF by default)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch font application completed.");
    }
}