SELECT t.Name, COUNT(ct.TypeId) from dbo.CardTypes ct 
join dbo.Types t on t.TypeId = ct.TypeId
GROUP BY t.Name
ORDER BY COUNT(ct.TypeId) DESC, t.Name ASC