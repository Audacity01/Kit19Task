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

--Updated Stored Proc
USE [kit19]
GO
/****** Object:  StoredProcedure [dbo].[SearchProducts]    Script Date: 04-12-2023 11:47:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchProducts]
    @ProductName NVARCHAR(255),
    @ProductNameConj VARCHAR(3) = 'AND',
    @productSize NVARCHAR(50),
    @SizeConj VARCHAR(3) = 'AND',
     @Price VARCHAR(10) ,
    @PriceConj VARCHAR(3) = 'AND',
    @MfgDate VARCHAR(10) ,
    @MfgDateConj VARCHAR(3) = 'AND',
    @Category NVARCHAR(100)
AS
BEGIN

   declare @sqlQuery nvarchar(max) 

    SET @sqlQuery = 'SELECT * FROM tbl_Product where 1=1 ';
    
   IF @ProductName IS NOT NULL
         SET @sqlQuery = @sqlQuery + 'AND  ProductName = @ProductName ';  
       
     IF @productSize IS NOT NULL
        SET @sqlQuery = @sqlQuery + @ProductNameConj + ' ProductSize = @productSize '  ;
        
     IF @Price IS NOT NULL
        SET @sqlQuery = @sqlQuery +   @SizeConj+ ' Price = @Price ' ;

     IF @MfgDate IS NOT NULL
        SET @sqlQuery = @sqlQuery   + @PriceConj + ' MfgDate = @MfgDate ' ;

     IF @Category IS NOT NULL
        SET @sqlQuery = @sqlQuery +   @MfgDateConj  + ' Category = @Category'   ;

         EXEC sp_executesql @sqlQuery
         ,  N'@ProductName NVARCHAR(255),
          @productSize NVARCHAR(50),
          @Price DECIMAL(18, 2),
          @MfgDate DATE,
          @Category NVARCHAR(100)',
        @ProductName,
        @productSize,
        @Price,
        @MfgDate,
        @Category;

END