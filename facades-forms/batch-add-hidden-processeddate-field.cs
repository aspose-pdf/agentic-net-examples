using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = "InputForms";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Generate a new GUID for this document
                string guidValue = Guid.NewGuid().ToString();

                // Prepare output file name (overwrite original)
                string outputPath = inputPath; // overwrite; change if a different name is desired

                // Use FormEditor to add a hidden text field with the GUID as its initial value
                using (FormEditor formEditor = new FormEditor())
                {
                    // Bind the source PDF
                    formEditor.BindPdf(inputPath);

                    // Add the hidden field on page 1; rectangle set to zero size (invisible)
                    // AddField(FieldType, fieldName, initValue, pageNum, llx, lly, urx, ury)
                    formEditor.AddField(FieldType.Text, "ProcessedDate", guidValue, 1, 0, 0, 0, 0);

                    // NOTE: The PropertyFlag.Hidden enum member does not exist in the current Aspose.PDF version.
                    // The field is already invisible because its rectangle has zero size, so no additional flag is required.
                    // If a future version adds a hidden flag, it can be set here using the appropriate enum value.

                    // Save the modified PDF (overwrites the original file)
                    formEditor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }
    }
}
