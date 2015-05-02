CREATE TRIGGER [Trigger]
	ON [dbo].[DeliveryChalan]
	AFTER  INSERT
	AS
	BEGIN
		UPDATE DeliveryChalan SET DCNo='EWJ/DC/'+CONVERT(varchar,Datepart(YEAR,SYSDATETIME()))+'/'+DateName(MONTH,SYSDATETIME())+'/'+CONVERT(varchar,DeliveryChalanID)  Where DeliveryChalanID IN(select DeliveryChalanID from inserted)
	END
	GO;
	CREATE TRIGGER [Trigger]
	ON [dbo].[WayBill]
	AFTER  INSERT
	AS
	BEGIN
		UPDATE WayBill SET DCNo='EWJ/WB/'+CONVERT(varchar,Datepart(YEAR,SYSDATETIME()))+'/'+DateName(MONTH,SYSDATETIME())+'/'+CONVERT(varchar,DeliveryChalanID)  Where DeliveryChalanID IN(select DeliveryChalanID from inserted)
	END