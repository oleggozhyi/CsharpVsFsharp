module UnitOfMeasure

   [<Measure>]  type m
   [<Measure>]  type s
   [<Measure>]  type kg
   [<Measure>]  type N = kg*m/s^2

   let mass = 10.0<kg>
   let acceleration = 9.8<m/s^2>
   //let force = mass + acceleration  <--- wont compile
   //let areEqual = mass = acceleration <--- wont compile

   let force1 = mass * acceleration
   let force2 = 50.0<N>
   let forceCombined = force1 + force2

   let calcForce (m: float<kg>) (a:float<m/s^2>)  = m * a

   //let x = calcForce 1.0 9.8 <--- wont compile
   let x = calcForce 1.0<kg> 9.8<m/s^2> 