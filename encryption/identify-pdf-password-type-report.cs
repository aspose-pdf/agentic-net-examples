using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum lives here in recent versions

class Program
{
    // Entry point
    static void Main()
    {
        // Paths to encrypted PDF files
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Known passwords (adjust as needed)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // List to hold report lines
        List<string> reportLines = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                reportLines.Add($"{pdfPath}: File not found");
                continue;
            }

            string passwordType = DeterminePasswordType(pdfPath, userPassword, ownerPassword);
            reportLines.Add($"{pdfPath}: {passwordType}");
        }

        // Write the report to a plain‑text file
        const string reportPath = "PasswordReport.txt";
        File.WriteAllLines(reportPath, reportLines);
        Console.WriteLine($"Report written to '{reportPath}'.");
    }

    // Attempts to open the PDF with the supplied passwords and returns the type
    static string DeterminePasswordType(string filePath, string userPwd, string ownerPwd)
    {
        // Try opening with the user password first
        try
        {
            using (Document doc = new Document(filePath, userPwd))
            {
                // If opened successfully, we need to decide whether the supplied password
                // was a true user password or the owner password used as a user password.
                // Owner password grants *all* permissions, while a regular user password
                // usually has some restrictions.
                if (HasFullPermissions(doc))
                    return "OwnerPassword (full access)";
                else
                    return "UserPassword (restricted access)";
            }
        }
        catch (InvalidPasswordException)
        {
            // Not a valid user password – try owner password
        }

        // Try opening with the owner password
        try
        {
            using (Document doc = new Document(filePath, ownerPwd))
            {
                // Owner password always provides full permissions
                return "OwnerPassword (full access)";
            }
        }
        catch (InvalidPasswordException)
        {
            // Neither password worked
            return "InvalidPassword or unknown type";
        }
    }

    // Checks whether the document grants *all* possible permissions.
    // The set of flags can differ between Aspose.Pdf versions, so we build the
    // "all‑flags" mask dynamically by OR‑ing every defined enum value.
    static bool HasFullPermissions(Document doc)
    {
        // Build a mask that contains every flag defined in the Permissions enum
        Permissions allPermissions = 0;
        foreach (Permissions perm in Enum.GetValues(typeof(Permissions)))
        {
            allPermissions |= perm;
        }

        // Document.Permissions is an int in many Aspose.Pdf versions; cast it to the enum
        Permissions docPermissions = (Permissions)doc.Permissions;

        // If the document's permissions contain every flag, we treat it as owner access.
        return (docPermissions & allPermissions) == allPermissions;
    }
}
