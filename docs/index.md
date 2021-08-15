<h1 style="text-align:center !important;"> SNHU Capstone Project Page </h1>
<h2 style="text-align:center !important;"> By Alan Davis </h2>
<h3 style="text-align:center !important;"> August 14th, 2021 </h3>

<h4>Intro / Professional Self-Assessment</h4>

<p>
    I have greatly enjoyed my career as a student in the SNHU Computer Science program. There was a lot to learn about theory, technology, and the application of programming principles in software development. Although my time in the program is coming to an end, and I will be happily graduating with my degree in Computer Science, I am aware that the professional field of technology is an ever-evolving space, and I will have to be proactive about learning new developments in the industry to keep my skills and coding practices sharp. 
</p>

<p>
  	There was a lot that I have learned about the underlying Computer Science fundamentals in this program. Specifically, the role that Data Structures and Algorithms play in software development. I believe that a lot of the fundamentals are being abstracted away with new technologies that make a developer’s life easy, however it is important to be aware of the fact that all of these new technologies are relying on the same fundamentals that got us to where we are here today with respect to all things Computer Science. So, having had the experience of learning about the fundamentals, I have attained a great appreciation of their importance and the role they play in the grand scheme of things. 
</p>

<p>
  	With regards to the capstone course, I am appreciative of the fact that I was able to bring all my knowledge together and demonstrate it in the form of updating an existing application that I have worked on in the past. It was a great recap of the many things I have learned thus far, and their application to an actual scenario, which I will be further updating in the future. 
</p>

<p>
  	I cannot wait to showcase my skills to my future employers and start my journey towards a career as a technology professional. 
</p>
   <br /><br />
 <h4> Milestone One - Code Review </h4>
 <br />
 <div style="text-align:center !important;">
 <iframe width="560" height="315" src="https://www.youtube.com/embed/5gHMVmOznog" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
    </div>
 <br /><br />
 
 <h4> Milestone Two - Enhancement One: Software Design/Engineering </h4>
 
 
<p>
  	The artifact I am including for this assignment is a code file that serves as the code-behind for the Controller Methods of the ASP.Net MVC web application project that is written in C#. This file contains methods for the various controller actions that are responsible for building the Model objects and returning them along with the Views to the user as a response to their HTTP request. This controller is responsible for the vast majority of the pages that the user will be interacting with, it was created as each individual View of the web application was created and required an associated controller method. 
</p>

<p>
  	This artifact was included because it showcases the majority of the logic that is used by the application, and it is a good example that showcases my ability to utilize various programming techniques to manipulate the behavior and output of the application based on user input. I have made several changes to the artifact that I have identified as beneficial to enhancing the code’s readability and maintainability, exception handling, as well as security of the application. 
</p>
<p>
  	Throughout the code, I added comments that enable both myself and future developers understand the functionality a code segment is attempting to achieve. I’ll include snippets from a diff view in my IDE to demonstrate the changes I’ve made below: 
</p>
<p>
  	In addition to adding comments that help the reader trace the code, I have also decoupled the generation of the model object from the View method that is being called by the controller method. This allows for better readability, because the reader will know what type of object is being generated for the View model.
</p>
<p>
  	Further, I made changes to my switch statement. Where before I used to return the View that had a LINQ expression generating the model object by querying the database through Entity Framework. Now, my switch statement no longer returns the view after each case, but assigns the value of the defined model object in the beginning of the controller method, and the object is then separately passed to the View method that is called at the return statement of the controller. This mitigates user error and code duplication, by reducing the number of times that a returned View method has to be defined after each ‘case’ of a switch statement. 
</p>
<p>
  	Another change that I have introduced to the artifact were more checks to determine whether the supplied model object is valid, that is, if it conforms to the model data definition, before the code continues any further. 
</p>
<p>
  	In the above snippet, you can see that I added logic to check whether the Model State of the supplied model object is valid. If it is not, then the user is returned to the original view that is making the request. Further, as I discussed in my code review, I now strive to follow better naming conventions, and therefore renamed the one of the objects from ‘item’ to ‘collectionObject’, which follows camel-case formatting, and enhances readability. Comments were also added here to make my intent clear, specifically as to why a separate object was declared of the same type as the model that was already passed into the method. In this case, it was to circumvent a vulnerability in this web-application known as ‘overposting’. 
</p>
<p>
  	Yet another change I made to the artifact involves additional checks around file operations. Specifically, whether the file already exists within a directory, before my application attempts to create another file with the same name inside a folder – thereby causing an exception. 
</p>
<p>
  	Here you can see that I added an if clause, which checks whether a file with the specified path already exists. If it does not, then the file is created within the system. Otherwise, an exception is thrown and is caught and processed by the ‘catch’ block of the code segment further down in the method. I also added comments to make this apparent. 
</p>
<p>
  	I believe that I have satisfied the course objectives defined in the earlier module. Part of writing good software is writing clear and concise code, which I think I am getting more proficient at, with better usage of comments and other programming practices that help both myself, and other developers who may look at this application down the line, quickly and seamlessly trace the code and understand my intent when I was originally writing this logic. 
</p>
<p>
  	While making changes to this artifact, I really enjoyed reflecting on my coding practices before my experience in the relevant Computer Science courses, and how my coding style and perspective has changed since then. What this assignment is, essentially, is a refactoring exercise. And one challenge I have encountered is the importance of being judicious of my time and effort invested when reviewing code. Because, since I have picked up many new coding techniques over time, as I was reviewing my code, many times I was tempted to overhaul the method and re-write it differently. However, a refactor does not mean a complete re-write of the application, performing this exercise made me appreciate the challenge of identifying potentially problematic code and having to trace through blocks of ambiguous and undocumented code, because I had forgotten what the code was trying to accomplish. Subsequently adding clear comments where applicable will serve me and possibly other developers well into the future.  
</p>
<br /><br />
<h4>Milestone Three – Algorithms and Data Structures</h4>
<p>
  	Module three afforded me an opportunity to revisit my implementation of Algorithms and Data Structures within the Web Application solution that I am using throughout this capstone course. The application is an ASP.Net Web Application that follows the MVC design pattern, coded in C# and utilizing Entity Framework as the ORM. I selected this application as my artifact, because in my initial creation of the application, I have not taken into consideration the importance of using correct types. So, although the performance implications from my changes in this module may be negligible for its current use case, if this application were to scale from processing a couple thousand objects to many millions, the performance implications would become more apparent. This is because lighter data types are less costly than their heavier counterparts i.e. using structs instead of classes, when applicable. Likewise, using correct algorithm to search through an array of objects is also important, because making the correct assumptions in advance, will yield faster search times from less processing. 
</p>
<p>
  	 In this narrative I will be focusing on the changes I have made to a specific type in my application, namely the ‘CollectionItemIdAndName’ class type. I noticed that within the application, I have a class type defined that only serves to house two properties within it. Defining the type as a class, may have been a mistake, since this type is only used to store values such as an integer reference to the ‘ID’ of a collectible item (i.e. action figure) that the object relates to. This class type is then stored within a dictionary object within the application, which is then used as a model object in the ‘Report’ view that displays statistical information to the user about their inventory. Since, this report view is generated by making calculations and aggregations on objects contained within the dictionary, accessing properties within a class object inside the dictionary would be additional processing that would have to occur to follow the references from the heap where the class type stores its references, onto the stack, where object’s values are actually stored. Thus, I have changed this object to a struct. In doing so, I am able to make the application more performant by accessing the values within the object’s properties directly on the stack without first having to reference the heap. A screenshot of the diff of my changes is represented below. Again, while this might be a trivial change for an application that only processes several thousand records, it would yield noticeable performance dividends if it was dealing with a much larger volume of objects. 
</p>
<p>
  	Another modification that I have made in this artifact that relates to making it more performant by choosing the correct data structure, is I have opted to use an Array type in place of a Generic List type that is provided in the .Net framework, where appropriate. I noticed that there is a View within the application that iterates over a collection of objects that is implemented via a List type. The List type available in .Net affords the programmer select features to work with the objects contained within the list. However, in this instance, I see that the code only iterates through the objects in the List, without taking advantage of any additional features the list has to offer. So, in the interest of refactoring to optimize, I have opted to change the List type to a lighter ‘Array’ type in the code, over which the loop will iterate. In effect, this should execute the loop faster through an Array type rather than a List type. 
</p>
<p>
  	I am very thankful that I can now be cognizant of the trade-offs between my design choices, and my selection of data types for a given application. This is one of the outcomes that is defined within the Computer Science program that I have definitely realized. I can not think of any apparent gaps within the coursework and its applicability, however one thing I have noticed, is that the coursework focuses a lot on the fundamentals of computer science, which is definitely important, and less on different technologies such as the .Net framework. Different frameworks may obscure a lot of the underlying plumbing to programmers, such as using different Data Structures within its native type system. So, it makes it slightly challenging to see how the different frameworks are implementing the data structures within their types, and which one is the best to select for a particular use case. I am happy I was able to experience and grow as a programmer through my career as a student in this program. 
</p>
<br /><br />
<h4>Milestone Three – Algorithms and Data Structures</h4>
<p>
  	
</p>
