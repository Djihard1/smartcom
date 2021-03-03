<template>
  <div>
    <h1>Product</h1>
    <hr>
    <h3>Filter Category</h3>
    <select v-model="filterCategory" @change="filtersByCategory(filterCategory)">
      <option>All</option>
      <option v-for="product in allProducts">{{product.category}}</option>
    </select>
    <hr>
    <div class="product" v-for="product in filtersProductCategory">
      <router-link :to="'/shop/product/' + product.id">
      <div class="productContainer">
        <img src="https://klike.net/uploads/posts/2019-06/1560664221_1.jpg">
        <br><br>
        <strong>{{product.name}}</strong>
        <p class="price">{{product.price}} </p>
      </div>
      </router-link>
    </div>
  </div>
</template>

<script>
    export default {
        name: 'ShopProduct',
        data () {
            return {
                allProducts:[],
                filterCategory:"",
                sortedallProducts:[]

            }
        },
        mounted(){
            //console.log("Mounted")
            this.init();
        },
        methods:{
            init(){
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.get("http://localhost:56750/api/Product/getall", ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.allProducts = res.data.data;

                    }
                })
            },
            filtersByCategory(category){
                let vm = this;
                this.sortedallProducts = []
                console.log(this.allProducts)
                this.allProducts.map(function (item) {
                    if (item.category === category)
                        vm.sortedallProducts.push(item)
                })
            },
        },
        computed:{
            filtersProductCategory(){
                if (this.sortedallProducts.length){
                    return this.sortedallProducts
                }
                else {
                    return this.allProducts
                }
            },
        }


    }
</script>



