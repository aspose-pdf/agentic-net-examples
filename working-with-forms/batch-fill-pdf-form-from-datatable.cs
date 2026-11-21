using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputDir = "GeneratedForms";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Simulated database records
        List<Record> records = GetMockRecords();

        foreach (Record record in records)
        {
            // Load a fresh copy of the template for each record
            using (Document pdf = new Document(templatePath))
            {
                Form form = pdf.Form;

                // Populate known text fields; adjust field names to match the template
                SetFieldValue(form, "FirstName", record.FirstName);
                SetFieldValue(form, "LastName",  record.LastName);
                SetFieldValue(form, "Email",     record.Email);
                SetFieldValue(form, "Date",      DateTime.Now.ToShortDateString());

                // Optional: make fields read‑only by flattening the form
                form.Flatten();

                string outputPath = Path.Combine(outputDir, $"Form_{record.Id}.pdf");
                pdf.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }
        }
    }

    // Safely assign a value to a TextBoxField if it exists
    static void SetFieldValue(Form form, string fieldName, string? value)
    {
        // The indexer returns a generic FormField; cast to TextBoxField when appropriate
        if (form[fieldName] is TextBoxField textBox && value != null)
        {
            textBox.Value = value;
        }
    }

    // Simple POCO representing a database row
    class Record
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

    // Mock data source; replace with real DB access as needed
    static List<Record> GetMockRecords()
    {
        return new List<Record>
        {
            new Record { Id = 1, FirstName = "Alice",   LastName = "Smith",   Email = "alice@example.com" },
            new Record { Id = 2, FirstName = "Bob",     LastName = "Johnson", Email = "bob@example.com"   },
            new Record { Id = 3, FirstName = "Carol",   LastName = "Williams",Email = "carol@example.com" }
        };
    }
}