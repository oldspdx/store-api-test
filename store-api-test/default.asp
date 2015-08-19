<!-- #include file="Json2Parser.asp" -->
<!DOCTYPE html>
<html>
<head>
    <title></title>
	<meta charset="utf-8" />
</head>
<body>
	<style>
		#catalogResults div {
			float: left;
			padding:20px;
			border: 1px solid blue;
			width:310px;
			margin-right:20px;
			margin-bottom:10px;
		}

        .productGrid div {
			float: left;
			padding:10px;
			border: 1px solid red;
			width:260px;
			margin-right:20px;
			margin-bottom:10px;
		}
		
		.menu
		{
			float: left;
			padding:5px;
			border: 1px solid red;
			width: 310px;
		}
		
		.findPortal
		{
			float: left;
			padding:10px;
		}
		
		.productGrid 
		{
			width: 70%;
			padding: 10px;
			border: 1px solid green;
			float: left;
			margin-left:10px;
			min-width: 200px;
		}

	</style>
	<%
	
	
 
	%>
	<form id="testForm" name="testForm" action="default.asp" method="post" >

	<%
		
		Dim myJSON 
		
		Dim strPortal : strPortal = request.querystring("portalID")
		Dim strCatalog : strCatalog = request.querystring("catalogID")
		Dim strCategoryID : strCategoryID = request.querystring("categoryID")
		Dim strProductID : strProductID = request.querystring("strProductID")
		
		Dim doSearch : doSearch = false
		
		Dim strPortalLookupTag : strPortalLookupTag = request.form("storeID")
		Dim strSearchWords : strSearchWords = request.form("searchtext")
		
		if (strPortal<="") and (request.form("search_button")="Search Products") then
			strPortal = request.form("existingPortal")
			doSearch = true
		end if

	%>


	<h2>Testing Web Service Call</h2>
	
	<h3>Select Store</h3>
	<br>
	<div class="findPortal">Portal Number or Store Name: </div>
	<div  class="findPortal">
		<input type="text" width="60" id="storeID" name="storeID"> 
		<small><i>(partial name OK)</i></small><br><br>
		<input type="hidden" id="existingPortal" name="existingPortal" value="<%=strPortal%>">
		<input type="Submit" value="Search">
	</div>
	<div style="display:block;clear:left"></div>
	<hr>
	
	<%
	
	if (strPortalLookupTag > "") then
		' lookup a dealer by portal ID or partial name and return results here
		Set myJSON = GetPortals(strPortalLookupTag)
			
		for each item in myJSON
			response.write "<div>"
			response.write "<a href=""default.asp?portalID=" & item.portalID & """ >Portal ID " & item.storeName & "</a><br>"
			response.write " Portal ID = "& item.portalID & "<br><br>"
			response.write "</div>"
		next
			
	%>  
		<hr>
	<%
	End if
	
	
	if (strPortal) then
		
		Set myJSON = GetCatalogsByPort(strPortal)
		
	%>
		<div id="catalogResults">
		<h3>Catalogs</h3>
	<%
			
		for each item in myJSON
			response.write "<div>"
			response.write item.name&"<br>"
			response.write "<a href=""default.asp?portalID=" & strPortal & "&catalogID=" & item.catalogID &""" > catalog ID " &item.catalogID&"</a><br>"
			response.write "</div>"
		next
	
	%>
		
		
		</div>
		<div style="clear:left;border:none;"></div>
		<div>
			<hr>
			<div class="findPortal">Search For: </div>
			<div  class="findPortal">
				<input type="text" width="60" id="searchtext" name="searchtext"> 
				<small><i>(partial name OK)</i></small><br><br>
				<input type="Submit" value="Search Products" id="search_button" name="search_button">
			</div>
			<div style="clear:left"></div>
			<hr>
		</div>
	<% 
	End if 
	%>


	<div> <!-- main container -->

	   <%
        
		if (strCatalog) then
			Set myJSON = GetCategoriesByCatalog(strCatalog)
		
			%>
			<div class="menu"> <!-- menu -->
				<%
				for each item in myJSON
					response.write "&raquo;" & "<a href=""default.asp?portalID=" & strPortal & "&catalogID=" & item.catalogID & "&categoryID=" & item.categoryID &""" >"  & item.title & "</a>" & " ID: "& item.categoryID &"<br>"
				next
			%>
			</div>
		
		 <%
		 End if
        
		if (strCategoryID) then
			Set myJSON = GetProductsByCategory(strCategoryID)
		
		%>
			<div class="productGrid"> <!-- products -->
				<%
			
				for each item in myJSON
					response.write "<div>"
					response.write item.title&"<br>"
					response.write " $ " & item.retailPrice&"<br>"
					response.write " ID " & item.productID&"<br>"
					response.write "</div>"
				
				next
			%>
			</div>
			
		<% 
		End if
		%>
		

		<%
		if (doSearch) then
			Set myJSON = SearchProducts(strPortal,strSearchWords)
		%>
			<div class="productGrid"> <!-- products -->
				<%
				if (myJSON=null) then
				Response.write " No data "
				End if
				for each item in myJSON
					response.write "<div>"
					response.write item.title&"<br>"
					response.write " $ " & item.retailPrice&"<br>"
					response.write " ID " & item.productID&"<br>"
					response.write "</div>"
				next
			%>
			</div>
			
		<% 
		End if
		%>
		
	</div>

</form>
</body>
</html>

<%
' function calls


	Function GetProductsByCategory (categoryID)
		Dim url
		url = "http://ds-dev-temp/api/product/category/" & categoryID
		Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP") 
		HttpReq.open "GET", url, false 
		HttpReq.setRequestHeader "Content-Type", "application/json"
		HttpReq.Send() 
		Set GetProductsByCategory = JSON.parse(HttpReq.responseText)
	End Function

	
	Function GetCategoriesByCatalog (catalogID)
		Dim url
		url = "http://ds-dev-temp/api/categorynode/" & catalogID
		Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP") 
		HttpReq.open "GET", url, false 
		HttpReq.setRequestHeader "Content-Type", "application/json"
		HttpReq.Send() 
		Set GetCategoriesByCatalog = JSON.parse(HttpReq.responseText)
	End Function
	

	Function GetCatalogsByPort (portID)
		Dim url
		url = "http://ds-dev-temp/api/portalcatalog/" & portID
		Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP") 
		HttpReq.open "GET", url, false 
		HttpReq.setRequestHeader "Content-Type", "application/json"
		HttpReq.Send() 
		Set GetCatalogsByPort = JSON.parse(HttpReq.responseText)
	End Function

	
	
	Function GetPortals (keyword)
		Dim url
		' crude integer check
		if (isNumeric(keyword)) then
			url = "http://ds-dev-temp/api/portal/" & keyword
		else
			keyword = Trim(keyword)
			' store name or partial name
			url = "http://ds-dev-temp/api/portal/storename/" & keyword
		end if
		
		Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP") 
		HttpReq.open "GET", url, false 
		HttpReq.setRequestHeader "Content-Type", "application/json"
		HttpReq.Send() 
		Set GetPortals = JSON.parse(HttpReq.responseText)
	End Function

	
	
	Function SearchProducts (portID, searchtext)
		Dim url
		url = "http://ds-dev-temp/api/product/portal/" & portID & "/keyword/" & searchtext
		Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP") 
		HttpReq.open "GET", url, false 
		HttpReq.setRequestHeader "Content-Type", "application/json"
		HttpReq.Send() 
		Set SearchProducts = JSON.parse(HttpReq.responseText)
	End Function

	
%>
