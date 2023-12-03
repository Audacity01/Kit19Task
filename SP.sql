USE [kit19]
GO
/****** Object:  StoredProcedure [dbo].[SearchProducts]    Script Date: 03-12-2023 12:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchProducts]
    @ProductName NVARCHAR(255) = NULL,
    @Size NVARCHAR(50) = NULL,
    @Price DECIMAL(18, 2) = NULL,
    @MfgDate DATE = NULL,
    @Category NVARCHAR(100) = NULL
AS
BEGIN
    SELECT *
    FROM dbo.tbl_Product
    WHERE
        (@ProductName IS NULL OR ProductName LIKE '%' + @ProductName + '%')
        AND (@Size IS NULL OR Size = @Size)
        AND (@Price IS NULL OR Price = @Price)
        AND (@MfgDate IS NULL OR MfgDate = @MfgDate)
        AND (@Category IS NULL OR Category = @Category);
END;