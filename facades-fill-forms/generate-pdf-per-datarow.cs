using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdf = "template.pdf";          // PDF with form fields
        const string outputDir   = "GeneratedPages";       // Folder for per‑row PDFs
        const string baseFileName = "RowPage";             // Base name for generated files

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // 1. Make sure a template PDF exists (create a minimal one if missing)
        // ------------------------------------------------------------
        EnsureTemplateExists(templatePdf);

        // ------------------------------------------------------------
        // 2. Build a sample DataTable (replace with real data source)
        // ------------------------------------------------------------
        DataTable table = new DataTable("Rows");
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Address", typeof(string));

        // Add a few rows – each row will become a separate PDF page
        table.Rows.Add("Alice", "123 Main St");
        table.Rows.Add("Bob",   "456 Oak Ave");
        table.Rows.Add("Carol", "789 Pine Rd");

        // ------------------------------------------------------------
        // 3. Generate one PDF per DataTable row using the modern API
        // ------------------------------------------------------------
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];

            // Load the template document
            Document pdf = new Document(templatePdf);

            // Fill form fields – adjust field names to match the template
            var nameField = pdf.Form?.Fields?.FirstOrDefault(f => f.FullName == "Name") as TextBoxField;
            if (nameField != null)
                nameField.Value = row["Name"].ToString();

            var addressField = pdf.Form?.Fields?.FirstOrDefault(f => f.FullName == "Address") as TextBoxField;
            if (addressField != null)
                addressField.Value = row["Address"].ToString();

            // Save the filled document as a separate file
            string outPath = Path.Combine(outputDir, $"{baseFileName}{i}.pdf");
            pdf.Save(outPath);
        }

        // ------------------------------------------------------------
        // 4. Verify each generated PDF contains exactly one page
        // ------------------------------------------------------------
        bool allValid = true;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string filePath = Path.Combine(outputDir, $"{baseFileName}{i}.pdf");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Missing file: {filePath}");
                allValid = false;
                continue;
            }

            using (Document doc = new Document(filePath))
            {
                if (doc.Pages.Count != 1)
                {
                    Console.WriteLine($"File '{filePath}' has {doc.Pages.Count} pages (expected 1).");
                    allValid = false;
                }
                else
                {
                    Console.WriteLine($"File '{filePath}' verified – 1 page.");
                }
            }
        }

        Console.WriteLine(allValid
            ? "Verification succeeded: each row produced a single‑page PDF."
            : "Verification failed: some PDFs do not have exactly one page.");
    }

    /// <summary>
    /// Creates a minimal PDF template with two text box fields (Name and Address) if the file does not already exist.
    /// </summary>
    private static void EnsureTemplateExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define rectangles for the form fields (left, bottom, right, top)
        var nameRect = new Rectangle(100, 700, 300, 720);
        var addressRect = new Rectangle(100, 650, 300, 670);

        // Create the text box fields
        TextBoxField nameField = new TextBoxField(page, nameRect)
        {
            PartialName = "Name",
            Value = ""
        };
        TextBoxField addressField = new TextBoxField(page, addressRect)
        {
            PartialName = "Address",
            Value = ""
        };

        // Add fields to the form – note that Form.Add expects the field and the 1‑based page number
        doc.Form.Add(nameField, 1);
        doc.Form.Add(addressField, 1);

        // Save the template
        doc.Save(path);
    }
}
