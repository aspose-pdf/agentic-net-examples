using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // For TextAnnotation and TextIcon

class ValidateFileSize
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";               // PDF containing the FileSelectBoxField
        const string outputPdfPath = "validated_form.pdf";    // Output PDF (optional)
        const string fieldName = "fileSelect";                // Name of the FileSelectBoxField in the form
        const long maxSizeBytes = 5L * 1024 * 1024;            // 5 MB limit

        // Verify that the source PDF exists before attempting to load it
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: Input PDF '{inputPdfPath}' not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the file‑select field by its name (nullable to silence CS8600 warning)
            FileSelectBoxField? fileField = pdfDoc.Form[fieldName] as FileSelectBoxField;

            if (fileField != null)
            {
                // The field value holds the selected file path (as a string)
                string selectedFilePath = fileField.Value;

                if (!string.IsNullOrEmpty(selectedFilePath) && File.Exists(selectedFilePath))
                {
                    long fileSize = new FileInfo(selectedFilePath).Length;

                    if (fileSize > maxSizeBytes)
                    {
                        // File exceeds the allowed size – take appropriate action
                        Console.WriteLine($"Error: Selected file \"{selectedFilePath}\" is {fileSize / (1024 * 1024)} MB, which exceeds the 5 MB limit.");

                        // Example: clear the field to prevent submission
                        fileField.Value = string.Empty;

                        // Optionally, add a visual warning annotation
                        Page firstPage = pdfDoc.Pages[1];
                        Aspose.Pdf.Rectangle warningRect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);
                        TextAnnotation warning = new TextAnnotation(firstPage, warningRect)
                        {
                            Title = "Validation",
                            Contents = "Selected file exceeds 5 MB limit.",
                            Color = Aspose.Pdf.Color.Red,
                            Open = true,
                            Icon = TextIcon.Note
                        };
                        firstPage.Annotations.Add(warning);
                    }
                    else
                    {
                        Console.WriteLine($"Selected file \"{selectedFilePath}\" is within the size limit.");
                    }
                }
                else
                {
                    Console.WriteLine("No file selected or file does not exist.");
                }
            }
            else
            {
                Console.WriteLine($"Field \"{fieldName}\" not found or is not a FileSelectBoxField.");
            }

            // Save the (potentially modified) document (lifecycle rule: use Save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine("Validation completed.");
    }
}
