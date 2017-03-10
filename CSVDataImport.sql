
DROP TABLE CSVMaterials

CREATE TABLE CSVMaterials
( f1 NVARCHAR(255),
f2 NVARCHAR(255),
f3 NVARCHAR(255),
f4 NVARCHAR(255),
f5 NVARCHAR(255),
f6 NVARCHAR(255))
GO

--SELECT * FROM OPENROWSET(BULK N'D:KoningMaterials - Sheet1.csv', SINGLE_CLOB) AS Contents

BULK
INSERT CSVMaterials
FROM 'D:KoningMaterials - Sheet1.txt'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n'
,DATAFILETYPE = 'widechar'
)
GO

SELECT * FROM CSVMaterials

CREATE TABLE #t (id INT,c1 VARCHAR(MAX),c2 NVARCHAR(MAX));
INSERT INTO #t VALUES(1,'žđšćč žđćčžđšćčŽĐŠĆČ','žđšćč žđćčžđšćčŽĐŠĆČ');
INSERT INTO #t VALUES(2,N'žđšćč žđćčžđšćčŽĐŠĆČ',N'žđšćč žđćčžđšćčŽĐŠĆČ');
SELECT * FROM #t;
DROP TABLE #t;

--INSERTING MATERIAL CATEGORIES
INSERT INTO MaterialCategories (IsArchived,Name)
SELECT 'False',f1 FROM CSVMaterials WHERE f2 IS NULL

--INSERTING MATERIALS
DECLARE @CategoryId as INT;
DECLARE @f1 NVARCHAR(255);
DECLARE @f2 NVARCHAR(255);
DECLARE @f3 NVARCHAR(255);
DECLARE @f4 NVARCHAR(255);
DECLARE @f5 NVARCHAR(255);
DECLARE @f6 NVARCHAR(255);


DECLARE @BusinessCursor as CURSOR;

SET @BusinessCursor = CURSOR FOR
Select * From [EnergetskiPregled].[dbo].[CSVMaterials]
 
OPEN @BusinessCursor;
FETCH NEXT FROM @BusinessCursor INTO @f1,@f2,@f3,@f4,@f5,@f6;

WHILE @@FETCH_STATUS = 0
BEGIN
	IF @f2 is NULL
		SET @CategoryId = (SELECT TOP 1 Id FROM MaterialCategories Where Name = @f1)
	ELSE
		INSERT INTO Materials (CategoryId,Name,Density,HeatConduction,SpecificHeat,RelativeWaterVaporDiffusionCoefficient,IsArchived)
		VALUES (@CategoryId
				,@f1
				,@f3
				,@f4
				,@f5
				,@f6
				,'False')
 FETCH NEXT FROM @BusinessCursor INTO @f1,@f2,@f3,@f4,@f5,@f6;
END

CLOSE @BusinessCursor;
DEALLOCATE @BusinessCursor;
