class Player extends PIXI.Graphics{
    constructor(radius=10, color=0x00FF00, x=0, y=0, speed=100,  height=10, width=10){
        super();
		this.x = x;
        this.y = y;
        this.radius = radius;        
        this.speed = speed;
        this.height = height;
        this.width = width;
        this.color = color;
        this.beginFill(color);
        //this.lineStyle(4, 0xFFFFFF, 1);
		this.drawCircle(0,0,radius);
		this.endFill();

        this.isAlive = true;

        this.dx = 0; 
        this.dy = 0;
        this.dr = 0;

    }

    update(dt){
		this.x += this.dx * dt;
        this.y += this.dy * dt;
	}

    changeR(dt, check){
        if(check == false)
        {
            this.height += this.dr * (dt * 2);
            this.width += this.dr * (dt * 2);
            if(this.height <= 10 && this.width <= 10)
            {
                this.height = 10;
                this.width = 10;
            }

            if(this.height >= 100 && this.width >= 100)
            {
                this.height = 100;
                this.width = 100;
            }
            //console.log(this.radius);
        }

        
    }

    border(){
        this.dx *= -1;
        this.dy *= -1;
    }

    shrink(dt){
        this.height += this.dr * (dt * 2);
        this.width += this.dr * (dt * 2);

        if(this.height <= 10 && this.width <= 10)
        {
            this.height = 10;
            this.width = 10;
        }

        if(this.height >= 100 && this.width >= 100)
        {
            this.height = 100;
            this.width = 100;
        }
    }
    
}

class Target extends PIXI.Graphics{
    constructor(radius=10, color=0xFF0000, x=0, y=0, speed=100, scale=1, height=10, width=10){
        super();
		this.x = x;
        this.y = y;
        this.radius = radius;        
        this.speed = speed;
		this.beginFill(color);
		this.drawCircle(0,0,radius);
		this.endFill();

        this.isAlive = true;

        this.dx = 0; 
        this.dy = 0;
        this.dr = 0;

    }


}

class Border extends PIXI.Graphics{
    constructor(width=2, height=10, color=0xFFFFFF, x=0, y=0){
        super();
        this.x = x;
        this.y = y;
        this.beginFill(color);
		this.drawRect(0,0,width,height);
		this.endFill();
    }
}