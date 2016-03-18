CREATE PROCEDURE [dbo].CreaturesByPower
AS
	SELECT c.Name, c.CreaturePower FROM dbo.Cards c
	join dbo.CardTags ct on c.CardId = ct.CardId
	WHERE ct.TagId = 4 -- Creature
	ORDER BY TRY_CONVERT(int, c.CreaturePower) DESC;
GO
