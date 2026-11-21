using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to inspect
        const string inputFolder = "pdfs";
        // Output CSV file
        const string summaryPath = "encryption_summary.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("FileName,IsEncrypted,Algorithm,Permissions");

        // Iterate over all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            bool isEncrypted = false;
            string algorithm = "None";
            string perms = "None";

            // Try to open the document without a password. If it throws PdfException the file is encrypted.
            try
            {
                using (var doc = new Document(pdfPath))
                {
                    // Not encrypted – attempt to read permissions via reflection (covers older SDK versions).
                    isEncrypted = false;
                    perms = GetPermissionsStringFromDocument(doc);
                }
            }
            catch (PdfException)
            {
                // Encrypted PDF – use PdfFileInfo to obtain the encryption algorithm (if the property exists).
                isEncrypted = true;
                var info = new PdfFileInfo(pdfPath);
                PropertyInfo algoProp = typeof(PdfFileInfo).GetProperty("EncryptionAlgorithm", BindingFlags.Public | BindingFlags.Instance);
                algorithm = algoProp?.GetValue(info)?.ToString() ?? "Unknown";
                perms = "Unknown"; // Permissions cannot be read without the password.
            }

            sb.AppendLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm},{perms}");
        }

        // Write the CSV summary
        File.WriteAllText(summaryPath, sb.ToString());
        Console.WriteLine($"Encryption summary saved to '{summaryPath}'.");
    }

    // Helper that extracts permission flags from a Document instance using reflection.
    // Works even when the Security or Permissions members are not present in the referenced SDK version.
    static string GetPermissionsStringFromDocument(Document doc)
    {
        // Default when we cannot locate the members.
        const string defaultResult = "None";

        // Try to get the 'Security' property.
        PropertyInfo securityProp = typeof(Document).GetProperty("Security", BindingFlags.Public | BindingFlags.Instance);
        if (securityProp == null)
            return defaultResult;

        object securityObj = securityProp.GetValue(doc);
        if (securityObj == null)
            return defaultResult;

        // Try to get the 'Permissions' property from the security object.
        PropertyInfo permsProp = securityObj.GetType().GetProperty("Permissions", BindingFlags.Public | BindingFlags.Instance);
        if (permsProp == null)
            return defaultResult;

        object permsValue = permsProp.GetValue(securityObj);
        if (permsValue == null)
            return defaultResult;

        // Convert the enum (or int) to a readable pipe‑separated list.
        int permsInt = (int)permsValue;
        var list = new List<string>();
        foreach (Permissions flag in Enum.GetValues(typeof(Permissions)))
        {
            if (flag == 0)
                continue;
            if ((permsInt & (int)flag) != 0)
                list.Add(flag.ToString());
        }
        return list.Count > 0 ? string.Join("|", list) : "None";
    }
}
