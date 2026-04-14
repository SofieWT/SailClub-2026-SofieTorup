--SELECT * FROM Boat WHERE SailNumber = @SailNumber
SELECT * FROM Boat WHERE SailNumber = '291-120';
Delete FROM Boat WHERE SailNumber = '291-120';

UPDATE Boat 
SET Id = 99, Model = 'Model02', EngineInfo = 'slow', Length = 1, Width = 1, Draft = 1, YearOfConstruction = 2000, TheBoatType = 'WAYFARER'
WHERE SailNumber = '291-120';

SELECT * FROM Boat WHERE SailNumber = '291-120';
--INSERT INTO Boat VALUES()
INSERT INTO Boat VALUES('FEVA', 'Model', '8877', 'fast', 2, 3, 4, 1900, 3);
INSERT INTO Boat VALUES('TERA', 'Model', '7788', 'fast', 2, 3, 4, 1900, 3);

UPDATE Boat 
SET Id = 2
WHERE SailNumber = '8877';